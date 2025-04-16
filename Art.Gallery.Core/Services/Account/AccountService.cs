using Art.Gallery.Common;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Account;
using Art.Gallery.Data.Entities.Account;
using Art.Gallery.Emails;
using Azure;
using Ganss.Xss;

namespace Art.Gallery.Core.Services.Account;
public class AccountService : IAccountService
{
    private readonly SiteDBContext _db;
    private readonly IMailSender _mailSender;
    public AccountService(SiteDBContext db, IMailSender mailSender)
    {
        _db = db;
        _mailSender = mailSender;
    }

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

    public AccountResult Login(LoginDto dto)
    {
        var user = _db
            .Users.FirstOrDefault(a => a.Email == dto.Email);

        if (user != null)
            return AccountResult.Null;

        return AccountResult.Success;
    }

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
        mail.Description = "کاربر گرامی ، ایمیل ارسالی به منظور بازیابی کلمه عبور برای شما ارسال شده است! در صورتی که شما درخواست مورد نظر را ثبت نکرده اید ، این مورد را گزارش دهید.";
        mail.ButtonTitle = "بازیابی کلمه عبور";
        mail.Link = PathExtension.DomainAddress + $"/Reset-Password/{user.ActiveCode}";

        var res =  _mailSender.SendEmail(mail);

        if (res == MailResult.Error)
            return AccountResult.Error;

        return AccountResult.Success;
    }

    public AccountResult Reset(ResetPasswordDto dto)
    {
        try
        {
            HtmlSanitizer san = new HtmlSanitizer();

            dto.Email = san.Sanitize(dto.Email);

            var user = _db.Users.FirstOrDefault(a => a.Email == dto.Email);

            if (user == null)
                return AccountResult.Null;

            string salt = Hashing.HashSalt($"{DateTime.Now.ToString()}{dto.Email}");
            string password = Hashing.HashPassword($"{dto.Password}{salt}");

            user.Password = san.Sanitize(password);
            user.Salt = salt;

            _db.Users.Update(user);
            _db.SaveChanges();

            return AccountResult.Success;
        }
        catch (Exception)
        {

            return AccountResult.Error;
        }
    }
}