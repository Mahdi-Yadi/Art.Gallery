using System.ComponentModel.DataAnnotations;
using Art.Gallery.Data.Entities.Common;
namespace Art.Gallery.Data.Entities.Account;
public class User : BaseEntity
{
    [MaxLength(100)]
    [Required]
    [DataType(DataType.Text)]
    public string UserName { get; set; }
    [MaxLength(150)]
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [MaxLength(300)]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool IsActive  { get; set; }
    public bool IsAdmin  { get; set; }
}