using Art.Gallery.Common;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Account;
using Art.Gallery.Data.Entities.Account;
using Azure;
using Ganss.Xss;

namespace Art.Gallery.Core.Services.Account;
public class AccountService : IAccountService
{
    private readonly SiteDBContext _db;

    public AccountService(SiteDBContext db)
    {
        _db = db;
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

        u.Email = san.Sanitize(dto.Email);
        u.Password = san.Sanitize(password);
        u.Salt = salt;
        u.UserName = san.Sanitize(dto.UserName);
        u.IsActive = false;
        u.IsAdmin = false;

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
        throw new NotImplementedException();
    }

    public AccountResult Reset(ResetPasswordDto dto)
    {
        throw new NotImplementedException();
    }
}