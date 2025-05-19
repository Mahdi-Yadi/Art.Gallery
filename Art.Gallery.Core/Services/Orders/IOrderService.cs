using Art.Gallery.Data.Dtos.Orders;
namespace Art.Gallery.Core.Services.Orders;
public interface IOrderService : IAsyncDisposable
{
    // Add Order
    OrderResult AddOrder(long productId, string userId);

}