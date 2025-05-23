using Art.Gallery.Common;
using Art.Gallery.Core.Services.Account;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Orders;
using Art.Gallery.Data.Dtos.Paging;
using Art.Gallery.Data.Entities.Orders;
using Microsoft.EntityFrameworkCore;
namespace Art.Gallery.Core.Services.Orders;
public class OrderService : IOrderService
{

    private readonly SiteDBContext _db;
    private readonly UrlProtector _urlProtector;
    private readonly IAccountService _accountService;

    public OrderService(SiteDBContext db, UrlProtector urlProtector, IAccountService accountService)
    {
        _db = db;
        _urlProtector = urlProtector;
        _accountService = accountService;
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

    public async Task<FilterOrdersDto> FilterOrders(FilterOrdersDto dto)
    {
        var query = _db.Orders.AsQueryable();

        if (!string.IsNullOrEmpty(dto.UserId))
        {
            if (!_accountService.IsAdmin(dto.UserId))
            {
                long usId = Convert.ToInt64(_urlProtector.UnProtect(dto.UserId));
                query = query.Where(a => a.UserId == usId);
            }
        }

        if (dto.OrderId != 0)
            query = query.Where(a => a.Id == dto.OrderId);

        var aQuery = query.Select(p => new
        {
            p.Id,
            p.CreateDate,
            p.PaymentDate,
            p.PaymentCode,
            p.Sum
        });

        var orders = (await aQuery.ToListAsync()).Select(p => new OrderDto()
        {
            Id = p.Id,
            Sum = p.Sum,
            CreateDate = p.CreateDate,
            PaymentCode = p.PaymentCode,
            PaymentDate = (DateTime)p.PaymentDate,
        }).ToList();

        #region Pagination

        var pager = Pager.Build(dto.PageId, await query.CountAsync(), dto.TakeEntity, dto.HowManyShowPageAfterAndBefore);

        dto.Count = orders.Count;

        return dto.SetOrders(orders).SetPaging(pager);

        #endregion
    }

    public OrderDto GetOrder(long orderId)
    {
        var o = _db
            .Orders
            .Include(a => a.OrderDetails)
            .ThenInclude(a => a.Product)
            .Include(a => a.User)
            .FirstOrDefault(a => a.Id == orderId);

        if (o == null)
            return null;

        OrderDto dto = new OrderDto();

        dto.Id = orderId;
        dto.PaymentCode = o.PaymentCode;
        if (o.OrderDetails.Count > 0 && o.PaymentCode == null)
        {
            dto.Sum = 0;
            foreach (var item in o.OrderDetails)
            {
                dto.Sum += (float)(item.Count * item.Product.Price);
            }
        }
        else
        {
            dto.Sum = o.Sum;
        }
        dto.CreateDate = o.CreateDate;
        dto.UserName = o.User.UserName;

        dto.OrderDetails = (List<OrderDetail>)o.OrderDetails;

        return dto;
    }

    public OrderDto GetOpenOrder(string userId)
    {
        long usId = Convert.ToInt64(_urlProtector.UnProtect(userId));

        var o = _db.Orders
            .Include(a => a.OrderDetails)
            .ThenInclude(a => a.Product)
            .FirstOrDefault(a => a.PaymentCode == null && a.UserId == usId);

        if (o == null) return null;

        OrderDto dto = new OrderDto();

        dto.Id = o.Id;
        dto.PaymentCode = o.PaymentCode;

        if (o.OrderDetails.Count > 0)
        {
            dto.Sum = 0;
            foreach (var item in o.OrderDetails)
            {
                dto.Sum += (float)(item.Count * item.Product.Price);
            }
        }

        dto.CreateDate = o.CreateDate;
        dto.UserName = o.User.UserName;

        dto.OrderDetails = (List<OrderDetail>)o.OrderDetails;

        return dto;
    }

    public bool UpdateOrderForPayment(long orderId,string trackingCode)
    {
        try
        {
            var o = _db.Orders.FirstOrDefault(a => a.Id == orderId);

            if (o == null) return false;

            o.TrackingCode = trackingCode;

            _db.Orders.Update(o);
            _db.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool UpdateOrderAfterPayment(string trackingCode, string paymentCode)
    {
        try
        {
            var o = _db.Orders.FirstOrDefault(a => a.TrackingCode == trackingCode);

            if (o == null) return false;

            o.PaymentCode = paymentCode;
            o.PaymentDate = DateTime.Now;

            _db.Orders.Update(o);
            _db.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_db != null)
            await _db.DisposeAsync();
    }
}