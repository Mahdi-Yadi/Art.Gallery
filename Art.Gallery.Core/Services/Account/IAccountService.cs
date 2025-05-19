using Art.Gallery.Data.Dtos.Account;
namespace Art.Gallery.Core.Services.Account;
public interface IAccountService
{

    AccountResult Register(RegisterDto dto);
    AccountResult Login(LoginDto dto);
    AccountResult Forgot(ForgotPasswordDto dto);
    AccountResult Reset(ResetPasswordDto dto);

    ProfileDto GetUserProfileForEdit(string id);
    AccountResult EditProfile(ProfileDto dto);

}