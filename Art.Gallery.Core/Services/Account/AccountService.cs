using Art.Gallery.Common;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Account;
using Art.Gallery.Data.Dtos.Paging;
using Art.Gallery.Data.Entities.Account;
using Art.Gallery.Emails;
using Ganss.Xss;
using Microsoft.EntityFrameworkCore;
namespace Art.Gallery.Core.Services.Account;
public class AccountService : IAccountService
{
    private readonly SiteDBContext _db;
    private readonly IMailSender _mailSender;
    private readonly UrlProtector _urlProtector;

    public AccountService(SiteDBContext db, IMailSender mailSender, UrlProtector urlProtector)
    {
        _db = db;
        _mailSender = mailSender;
        _urlProtector = urlProtector;
    }


    // ثبت نام
    public AccountResult Register(RegisterDto dto)
    {
        var user = _db
            .Users.FirstOrDefault(a => a.Email == dto.Email);

        if (user != null)
            return AccountResult.Exist;

        User u = new User();
        HtmlSanitizer san = new HtmlSanitizer();

        string salt = Hashing.HashSalt($"{DateTime.Now.ToString()}{dto.Email}");
        string password = Hashing.HashPassword($"{dto.Password}{salt}");

        DateTime currentDate = DateTime.Now;

        u.Email = san.Sanitize(dto.Email);
        u.Password = san.Sanitize(password);
        u.Salt = salt;
        u.UserName = san.Sanitize(dto.UserName);
        u.IsActive = false;
        u.IsAdmin = false;
        u.CreateDate = currentDate;
        u.UpdateDate = currentDate;
        u.ActiveCode = currentDate.ToString("ssMMHHyyyyddmm");

        _db.Users.Add(u);
        _db.SaveChanges();

        return AccountResult.Success;
    }

    // ورود
    public AccountResult Login(LoginDto dto)
    {
        var user = _db
            .Users.FirstOrDefault(a => a.Email == dto.Email);

        if (user != null)
            return AccountResult.Null;

        return AccountResult.Success;
    }

    // فراموشی کلمه عبور
    public AccountResult Forgot(ForgotPasswordDto dto)
    {
        HtmlSanitizer san = new HtmlSanitizer();

        dto.Email = san.Sanitize(dto.Email);

        var user = _db.Users.FirstOrDefault(a => a.Email == dto.Email);

        if (user == null)
            return AccountResult.Null;

        MailDTO mail = new MailDTO();

        mail.Email = user.Email;
        mail.Title = "فراموشی کلمه عبور";
        mail.Description =
            "کاربر گرامی ، ایمیل ارسالی به منظور بازیابی کلمه عبور برای شما ارسال شده است! در صورتی که شما درخواست مورد نظر را ثبت نکرده اید ، این مورد را گزارش دهید.";
        mail.ButtonTitle = "بازیابی کلمه عبور";
        mail.Link = PathExtension.DomainAddress + $"/Reset-Password/{user.ActiveCode}";

        var res = _mailSender.SendEmail(mail);

        if (res == MailResult.Error)
            return AccountResult.Error;

        return AccountResult.Success;
    }

    // بازگرداندن کلمه عبور
    public AccountResult Reset(ResetPasswordDto dto)
    {
        try
        {
            HtmlSanitizer san = new HtmlSanitizer();

            dto.Email = san.Sanitize(dto.Email);
            dto.ActiveCode = san.Sanitize(dto.ActiveCode);

            var user = _db.Users.FirstOrDefault(a => a.Email == dto.Email
                                                     && a.ActiveCode == dto.ActiveCode);

            if (user == null)
                return AccountResult.Null;

            string salt = Hashing.HashSalt($"{DateTime.Now.ToString()}{dto.Email}");
            string password = Hashing.HashPassword($"{dto.Password}{salt}");

            user.Password = san.Sanitize(password);
            user.Salt = salt;

            _db.Users.Update(user);
            _db.SaveChanges();

            MailDTO mail = new MailDTO();

            mail.Email = user.Email;
            mail.Title = "بروزرسانی حساب";
            mail.Description = "کلمه عبور شما با موفقیت ویرایش شد.";
            mail.ButtonTitle = "وب سایت";
            mail.Link = PathExtension.DomainAddress;

            var res = _mailSender.SendEmail(mail);

            return AccountResult.Success;
        }
        catch (Exception)
        {

            return AccountResult.Error;
        }
    }

    // دریافت اطلاعات کاربر برای ویرایش
    public ProfileDto GetUserProfileForEdit(string id)
    {
        long userId = Convert.ToInt64(_urlProtector.UnProtect(id));

        var user = _db.Users.FirstOrDefault(u => u.Id == userId);

        if (user == null)
            return new ProfileDto();

        ProfileDto dto = new ProfileDto();

        dto.Id = _urlProtector.Protect(user.Id.ToString());
        dto.PhoneNumber = user.PhoneNumber;
        dto.Email = user.Email;
        dto.Addrress = user.Addrress;
        dto.CodePost = user.CodePost;
        dto.UserName = user.UserName;

        return dto;
    }
  
    // ویرایش حساب کاربری 
    public AccountResult EditProfile(ProfileDto dto)
    {
        try
        {
            long userId = Convert.ToInt64(_urlProtector.UnProtect(dto.Id));

            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return AccountResult.Null;

            user.PhoneNumber = dto.PhoneNumber;
            user.Email = dto.Email;
            user.Addrress = dto.Addrress;
            user.CodePost = dto.CodePost;
            user.UserName = dto.UserName;

            _db.Users.Update(user);
            _db.SaveChanges();
            return AccountResult.Success;
        }
        catch (Exception)
        {
            return AccountResult.Error;
        }
    }

    // بررسی سرپرست بودن کاربر
    public bool IsAdmin(string id)
    {
        long userId = Convert.ToInt64(_urlProtector.UnProtect(id));

        var user = _db.Users.FirstOrDefault(a => a.Id == userId);

        if (user == null) return false;

        if(user.IsAdmin) return true;

        return false;
    }

    public async Task<FilterUsersDto> FilterUsers(FilterUsersDto dto)
    {
        var query = _db.Users
            .Include(a => a.Orders)
            .ThenInclude(a => a.OrderDetails)
            .Include(a => a.Artists)
            .AsQueryable();

        var aQuery = query.Select(p => new
        {
            p.Id,
            p.UserName,
            p.Email,
            p.PhoneNumber,
            p.IsDelete
        });

        var users = (await aQuery.ToListAsync()).Select(p => new User()
        {
            Id = p.Id,
            UserName = p.UserName,
            Email = p.Email,
            PhoneNumber = p.PhoneNumber,
        }).ToList();

        #region Pagination

        var pager = Pager.Build(dto.PageId, await query.CountAsync(), dto.TakeEntity, dto.HowManyShowPageAfterAndBefore);

        dto.Count = users.Count;

        return dto.SetUsers(users).SetPaging(pager);

        #endregion
    }

}