using Art.Gallery.Common;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Orders;
using Art.Gallery.Data.Entities.Orders;

namespace Art.Gallery.Core.Services.Orders;
public class OrderService : IOrderService
{

    private readonly SiteDBContext _db;
    private readonly UrlProtector _urlProtector;

    public OrderService(SiteDBContext db, UrlProtector urlProtector)
    {
        _db = db;
        _urlProtector = urlProtector;
    }


    public OrderResult AddOrder(long productId, string userId)
    {
        try
        {
            long usId = Convert.ToInt64(_urlProtector.UnProtect(userId));

            Order o = OrderCreator(usId);

            if (o == null)
                return OrderResult.Error;

            OrderDetail od = OrderDetailCreator(o.Id, productId);

            if (od == null)
                return OrderResult.Error;

            return OrderResult.Success;
        }
        catch (Exception)
        {
            return OrderResult.Error;
        }
    }

    private Order OrderCreator(long userId)
    {
        try
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null) return null;

            var oldOrder = _db.Orders.FirstOrDefault(a => a.UserId == user.Id);

            if (oldOrder == null)
            {
                Order o = new Order();

                o.UserId = userId;
                o.CreateDate = DateTime.Now;

                _db.Orders.Add(o);
                _db.SaveChanges();

                return o;
            }

            return oldOrder;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private OrderDetail OrderDetailCreator(long orderId, long productId)
    {
        try
        {
            var p = _db.Products.FirstOrDefault(a => a.Id == productId);

            if (p == null) return null;

            var orderDetail = _db.OrderDetails.FirstOrDefault(a => a.OrderId == orderId && a.ProductId == productId);

            if (orderDetail == null)
            {
                OrderDetail od = new OrderDetail();

                od.OrderId = orderId;
                od.ProductId = productId;
                od.Count = 1;

                _db.OrderDetails.Add(od);
                _db.SaveChanges();

                return od;
            }

            orderDetail.Count += 1;

            _db.OrderDetails.Update(orderDetail);
            _db.SaveChanges();

            return orderDetail;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_db != null)
            await _db.DisposeAsync();
    }
}