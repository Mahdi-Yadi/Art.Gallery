using Art.Gallery.Data.Dtos.Orders;
namespace Art.Gallery.Core.Services.Orders;
public interface IOrderService : IAsyncDisposable
{


    // Delete
    OrderResult DeleteProductFromOrder(long orderDetailId, string userId);

    // Add Order
    OrderResult AddOrder(string productId, string userId);

    // Filter
    
    Task<FilterOrdersDto> FilterOrders(FilterOrdersDto dto);

    OrderDto GetOrder(long orderId);

    OrderDto GetOpenOrder(string userId);

    bool UpdateOrderForPayment(long orderId, string trackingCode);

    bool UpdateOrderForComplete(long orderId);

    bool UpdateOrderForNotComplete(long orderId);

    bool UpdateOrderAfterPayment(string trackingCode,string paymentCode);

}