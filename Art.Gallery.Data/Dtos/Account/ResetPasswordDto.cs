using System.ComponentModel.DataAnnotations;
namespace Art.Gallery.Data.Dtos.Account;
public class ResetPasswordDto
{
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