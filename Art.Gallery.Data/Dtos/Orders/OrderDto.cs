using Art.Gallery.Data.Entities.Orders;

namespace Art.Gallery.Data.Dtos.Orders;
public class OrderDto
{
    public int Id { get; set; }

    public float Sum { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentCode { get; set; }
    
    public string UserName { get; set; }

    public List<OrderDetail> OrderDetails { get; set; }
}
public enum OrderResult
{
    Success,
    AddProductToOrder,
    Error,
    Nulls
}