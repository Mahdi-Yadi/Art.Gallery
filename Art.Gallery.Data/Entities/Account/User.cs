using System.ComponentModel.DataAnnotations;
using Art.Gallery.Data.Entities.Artists;
using Art.Gallery.Data.Entities.Common;
using Art.Gallery.Data.Entities.Orders;

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

    [MaxLength(500)]
    [DataType(DataType.Text)]
    public string Addrress { get; set; }

    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string CodePost { get; set; }

    [MaxLength(15)]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    [MaxLength(300)]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [MaxLength(50)]
    [DataType(DataType.Text)]
    public string ActiveCode { get; set; }

    [MaxLength(300)]
    [Required]
    [DataType(DataType.Password)]
    public string Salt { get; set; }

    public bool IsActive  { get; set; }

    public bool IsAdmin  { get; set; }

    public ICollection<Order> Orders { get; set; }

    public ICollection<Artist> Artists { get; set; }

}