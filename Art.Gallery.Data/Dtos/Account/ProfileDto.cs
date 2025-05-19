using System.ComponentModel.DataAnnotations;
namespace Art.Gallery.Data.Dtos.Account;
public class ProfileDto
{
    public string Id { get; set; }

    [MaxLength(100)]
    [Required]
    [DataType(DataType.Text)]
    public string UserName { get; set; }

    [MaxLength(150)]
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [MaxLength(500)]
    [DataType(DataType.Text)]
    public string Addrress { get; set; }

    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string CodePost { get; set; }

    [MaxLength(15)]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

}