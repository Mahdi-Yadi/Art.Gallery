using Art.Gallery.Data.Dtos.Account;
namespace Art.Gallery.Core.Services.Account;
public interface IAccountService
{

    AccountResult Register(RegisterDto dto);
    AccountResult Login(LoginDto dto);
    AccountResult Forgot(LoginDto dto);
    AccountResult Reset(LoginDto dto);

}