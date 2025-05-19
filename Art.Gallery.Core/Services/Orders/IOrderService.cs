using Art.Gallery.Data.Dtos.Orders;
namespace Art.Gallery.Core.Services.Orders;
public interface IOrderService : IAsyncDisposable
{
    // Add Order
    OrderResult AddOrder(long productId, string userId);

    Task<FilterOrdersDto> FilterOrders(FilterOrdersDto dto);

    OrderDto GetOrder(long orderId);

    OrderDto GetOpenOrder(string userId);

    bool UpdateOrderForPayment(long orderId, string trackingCode);

    bool UpdateOrderAfterPayment(string trackingCode,string paymentCode);

}