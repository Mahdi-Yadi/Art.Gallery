using Art.Gallery.Data.Dtos.Account;
namespace Art.Gallery.Core.Services.Account;
public interface IAccountService
{

    AccountResult Register(RegisterDto dto);

    AccountResult ActiveAccount(string activeCode);

    AccountResult Login(LoginDto dto);

    AccountResult Forgot(ForgotPasswordDto dto);

    AccountResult Reset(ResetPasswordDto dto);

    ProfileDto GetUserProfileForEdit(string id);

    AccountResult EditProfile(ProfileDto dto);

    bool IsAdmin(string id);

    Task<FilterUsersDto> FilterUsers(FilterUsersDto dto);

    bool AddAdmin(string userId);

    bool RemoveAdmin(string userId);

}