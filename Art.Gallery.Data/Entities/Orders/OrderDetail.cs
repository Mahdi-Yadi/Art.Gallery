using Art.Gallery.Data.Entities.Account;
using Art.Gallery.Data.Entities.Products;
using System.ComponentModel.DataAnnotations;
namespace Art.Gallery.Data.Entities.Orders;
public class OrderDetail
{
    [Key]
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long ProductId { get; set; }

    public int Count { get; set; }

    public Order Order { get; set; }

    public User User { get; set; }

    public Product Product { get; set; }
}