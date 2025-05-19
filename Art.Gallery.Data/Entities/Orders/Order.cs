using System.ComponentModel.DataAnnotations;
using Art.Gallery.Data.Entities.Account;

namespace Art.Gallery.Data.Entities.Orders;
public class Order
{
    [Key]
    public long Id { get; set; }

    public long UserId { get; set; }

    public float Sum { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string PaymentCode { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }

    public User User { get; set; }
}