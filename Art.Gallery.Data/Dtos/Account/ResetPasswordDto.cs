using System.ComponentModel.DataAnnotations;
namespace Art.Gallery.Data.Dtos.Account;
public class ResetPasswordDto
{
    [MaxLength(300, ErrorMessage = "ActiveCode نمی تواند بیشتر از 150 حرف باشد.")]
    [Required(ErrorMessage = "ActiveCode را وارد کنید")]
    [DataType(DataType.Text)]
    public string ActiveCode { get; set; }
    [MaxLength(150, ErrorMessage = "ایمیل نمی تواند بیشتر از 150 حرف باشد.")]
    [Required(ErrorMessage = "ایمیل را وارد کنید")]
    [DataType(DataType.EmailAddress, ErrorMessage = "لطفاایمیل را به درستی وارد کنید.")]
    public string Email { get; set; }
    [MaxLength(300, ErrorMessage = "کلمه عبور نمی تواند بیشتر از 150 حرف باشد.")]
    [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
    [DataType(DataType.Password, ErrorMessage = "لطفا کلمه عبور را به درستی وارد کنید.")]
    public string Password { get; set; }
    [MaxLength(300, ErrorMessage = "کلمه عبور نمی تواند بیشتر از 150 حرف باشد.")]
    [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
    [DataType(DataType.Password, ErrorMessage = "لطفا کلمه عبور را به درستی وارد کنید.")]
    [Compare("Password", ErrorMessage = "لطفا کلمه عبور را به درستی وارد کنید.")]
    public string RePassword { get; set; }
}