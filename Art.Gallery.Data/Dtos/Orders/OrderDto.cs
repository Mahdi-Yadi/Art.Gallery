using Art.Gallery.Data.Entities.Orders;
namespace Art.Gallery.Data.Dtos.Orders;
public class OrderDto
{
    public long OrderId { get; set; }

    public float Sum { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentCode { get; set; }

    public string TrackingCode { get; set; }

    public string UserName { get; set; }

    public List<OrderDetailDto> OrderDetailsDto { get; set; }
}
public class OrderDetailDto
{
    public string OrderDetailId { get; set; }

    public string ProductId { get; set; }

    public int Count { get; set; }

    public string ProductName { get; set; }

    public string ProductImage { get; set; }

    public decimal Price { get; set; }
}
public enum OrderResult
{
    Success,
    AddProductToOrder,
    Error,
    Nulls
}