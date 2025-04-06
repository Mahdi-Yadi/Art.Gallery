using System.ComponentModel.DataAnnotations;
namespace Art.Gallery.Data.Dtos.Account;
public class LoginDto
{
    [MaxLength(150, ErrorMessage = "ایمیل نمی تواند بیشتر از 150 حرف باشد.")]
    [Required(ErrorMessage = "ایمیل را وارد کنید")]
    [DataType(DataType.EmailAddress, ErrorMessage = "لطفاایمیل را به درستی وارد کنید.")]
    public string Email { get; set; }
    [MaxLength(300, ErrorMessage = "کلمه عبور نمی تواند بیشتر از 150 حرف باشد.")]
    [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
    [DataType(DataType.Password, ErrorMessage = "لطفا کلمه عبور را به درستی وارد کنید.")]
    public string Password { get; set; }
}
public enum AccountResult
{
    Success,
    Null,
    Error
}