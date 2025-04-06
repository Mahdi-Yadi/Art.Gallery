using System.ComponentModel.DataAnnotations;
namespace Art.Gallery.Data.Dtos.Account;
public class ForgotPasswordDto
{
    [MaxLength(150, ErrorMessage = "ایمیل نمی تواند بیشتر از 150 حرف باشد.")]
    [Required(ErrorMessage = "ایمیل را وارد کنید")]
    [DataType(DataType.EmailAddress, ErrorMessage = "لطفاایمیل را به درستی وارد کنید.")]
    public string Email { get; set; }
}