using Art.Gallery.Common;
using Art.Gallery.Core.Services.Account;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Orders;
using Art.Gallery.Data.Dtos.Paging;
using Art.Gallery.Data.Entities.Orders;
using Art.Gallery.Emails;
using Microsoft.EntityFrameworkCore;
namespace Art.Gallery.Core.Services.Orders;
public class OrderService : IOrderService
{

    private readonly SiteDBContext _db;
    private readonly UrlProtector _urlProtector;
    private readonly IAccountService _accountService;
    private readonly IMailSender _mailSender;

    public OrderService(SiteDBContext db,
        UrlProtector urlProtector,
        IAccountService accountService,
        IMailSender mailSender)
    {
        _db = db;
        _urlProtector = urlProtector;
        _accountService = accountService;
        _mailSender = mailSender;
    }

    // حذف ریز فاکتور
    public OrderResult DeleteProductFromOrder(string orderDetailId, string userId)
    {
        long usId = Convert.ToInt64(_urlProtector.UnProtect(userId));
        long odId = Convert.ToInt64(_urlProtector.UnProtect(orderDetailId));

        var user = _db.Users.FirstOrDefault(u => u.Id == usId);

        if (user == null) return OrderResult.Nulls;

        var od = _db.OrderDetails.
            Include(a => a.Order)
            .FirstOrDefault(a => a.Id == odId);

        if (od == null) return OrderResult.Nulls;

        // بررسی ریز فاکتور

        if (od.Order.PaymentCode != null || od.Order.UserId != user.Id)
            return OrderResult.Error;

        _db.OrderDetails.Remove(od);
        _db.SaveChanges();

        MailDTO mailDTO = new MailDTO();

        mailDTO.Title = "حذف کالا از فاکتور";
        mailDTO.CreateDate = DateTime.Now;
        mailDTO.Description = "فاکتور شما با موفقیت بروزرسانی شد و کالایی در از حذف گردید.";
        mailDTO.Email = user.Email;

        _mailSender.SendEmail(mailDTO);

        return OrderResult.Success;
    }

    // افزودن فاکتور

    public OrderResult AddOrder(string productId, string userId)
    {
        try
        {
            long usId = Convert.ToInt64(_urlProtector.UnProtect(userId));
            long pId = Convert.ToInt64(_urlProtector.UnProtect(productId));

            Order o = OrderCreator(usId);

            if (o == null)
                return OrderResult.Error;

            OrderDetail od = OrderDetailCreator(o.Id, pId);

            if (od == null)
                return OrderResult.Error;

            return OrderResult.Success;
        }
        catch (Exception)
        {
            return OrderResult.Error;
        }
    }

    // افزودن فاکتور اصلی

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

                MailDTO mailDTO = new MailDTO();

                mailDTO.Title = "ایجاد فاکتور";
                mailDTO.CreateDate = DateTime.Now;
                mailDTO.Description = "فاکتور شما با موفقیت ایجاد شد.";
                mailDTO.Email = user.Email;

                _mailSender.SendEmail(mailDTO);

                return o;
            }

            return oldOrder;
        }
        catch (Exception)
        {
            return null;
        }
    }

    // افزودن ریز فاکتور به فاکتور اصلی

    private OrderDetail OrderDetailCreator(long orderId, long productId)
    {
        try
        {
            var p = _db.Products.FirstOrDefault(a => a.Id == productId);

            if (p == null) return null;

            var orderDetail = _db
                .OrderDetails
                .Include(o => o.Order)
                .ThenInclude(o => o.User)
                .FirstOrDefault(a => a.OrderId == orderId && a.ProductId == productId);

            MailDTO mailDTO = new MailDTO();

            if (orderDetail == null)
            {
                OrderDetail od = new OrderDetail();

                od.OrderId = orderId;
                od.ProductId = productId;
                od.Count = 1;

                _db.OrderDetails.Add(od);
                _db.SaveChanges();

                var o = _db.Orders.Include(a => a.User).FirstOrDefault(a => a.Id == orderId);

                mailDTO.Title = "بروزرسانی فاکتور";
                mailDTO.CreateDate = DateTime.Now;
                mailDTO.Description = "فاکتور شما با موفقیت بروز شد.";
                mailDTO.Email = orderDetail.Order.User.Email;

                _mailSender.SendEmail(mailDTO);

                return od;
            }

            orderDetail.Count += 1;

            _db.OrderDetails.Update(orderDetail);
            _db.SaveChanges();

            mailDTO.Title = "بروزرسانی فاکتور";
            mailDTO.CreateDate = DateTime.Now;
            mailDTO.Description = "فاکتور شما با موفقیت بروز شد.";
            mailDTO.Email = orderDetail.Order.User.Email;

            _mailSender.SendEmail(mailDTO);

            return orderDetail;
        }
        catch (Exception)
        {
            return null;
        }
    }

    // فیلتر لیست فاکتور ها هم برای ادمین و هم برای کاربران
    public async Task<FilterOrdersDto> FilterOrders(FilterOrdersDto dto)
    {
        var query = _db.Orders.AsQueryable();

        if (!string.IsNullOrEmpty(dto.UserId))
            if (!_accountService.IsAdmin(dto.UserId))
            {
                long usId = Convert.ToInt64(_urlProtector.UnProtect(dto.UserId));
                query = query.Where(a => a.UserId == usId);
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
            OrderId = p.Id,
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

    // گرفتن یک فاکتور
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

        List<OrderDetailDto> orderDetails = new List<OrderDetailDto>();

        dto.OrderId = orderId;
        dto.PaymentCode = o.PaymentCode;
        if (o.OrderDetails.Count > 0 && o.PaymentCode == null)
        {
            dto.Sum = 0;
            foreach (var item in o.OrderDetails)
            {
                dto.Sum += (float)(item.Count * item.Product.Price);
                var od = new OrderDetailDto()
                {
                    Count = item.Count,
                    OrderDetailId = _urlProtector.Protect(item.Id.ToString()),
                    Price = (decimal)item.Product.Price,
                    ProductId = _urlProtector.Protect(item.ProductId.ToString()),
                };
                orderDetails.Add(od);
            }
        }
        else
        {
            dto.Sum = o.Sum;
        }
        dto.CreateDate = o.CreateDate;
        dto.UserName = o.User.UserName;

        return dto;
    }

    public OrderDto GetOpenOrder(string userId)
    {
        long usId = Convert.ToInt64(_urlProtector.UnProtect(userId));

        var o = _db.Orders
            .Include(a => a.User)
            .Include(a => a.OrderDetails)
            .ThenInclude(a => a.Product)
            .FirstOrDefault(a => a.PaymentCode == null && a.UserId == usId);

        if (o == null) return null;

        OrderDto dto = new OrderDto();

        dto.OrderId = o.Id;
        dto.PaymentCode = o.PaymentCode;

        List<OrderDetailDto> orderDetails = new List<OrderDetailDto>();

        if (o.OrderDetails.Count > 0)
        {
            dto.Sum = 0;
            foreach (var item in o.OrderDetails)
            {
                dto.Sum += (float)(item.Count * item.Product.Price);
                var od = new OrderDetailDto()
                {
                    Count = item.Count,
                    OrderDetailId = _urlProtector.Protect(item.Id.ToString()),
                    Price = (decimal)item.Product.Price,
                    ProductId = _urlProtector.Protect(item.ProductId.ToString()),
                };
                orderDetails.Add(od);
            }
        }

        dto.CreateDate = o.CreateDate;
        dto.UserName = o.User.UserName;

        return dto;
    }

    public bool UpdateOrderForPayment(long orderId, string trackingCode)
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

    public bool UpdateOrderForComplete(long orderId)
    {
        try
        {
            var o = _db.Orders
                .Include(a => a.User).FirstOrDefault(a => a.Id == orderId);

            if (o == null) return false;

            o.IsComplete = true;

            _db.Orders.Update(o);
            _db.SaveChanges();


            MailDTO mailDTO = new MailDTO();

            mailDTO.Title = "تکمیل فاکتور";
            mailDTO.CreateDate = DateTime.Now;
            mailDTO.Description = "فاکتور شما با موفقیت تکمیل شد.";
            mailDTO.Email = o.User.Email;

            _mailSender.SendEmail(mailDTO);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool UpdateOrderForNotComplete(long orderId)
    {
        try
        {
            var o = _db
                .Orders
                .Include(a => a.User)
                .FirstOrDefault(a => a.Id == orderId);

            if (o == null) return false;

            o.IsComplete = false;

            _db.Orders.Update(o);
            _db.SaveChanges();

            MailDTO mailDTO = new MailDTO();

            mailDTO.Title = "لغو تکمیل فاکتور";
            mailDTO.CreateDate = DateTime.Now;
            mailDTO.Description = "فاکتور شما با لغو تکمیل شد.";
            mailDTO.Email = o.User.Email;

            _mailSender.SendEmail(mailDTO);

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
            var o = _db.Orders
                .Include(a => a.User).FirstOrDefault(a => a.TrackingCode == trackingCode);

            if (o == null) return false;

            o.PaymentCode = paymentCode;
            o.PaymentDate = DateTime.Now;

            _db.Orders.Update(o);
            _db.SaveChanges();


            MailDTO mailDTO = new MailDTO();

            mailDTO.Title = "پرداخت فاکتور";
            mailDTO.CreateDate = DateTime.Now;
            mailDTO.Description = "فاکتور شما با موفقیت پرداخت شد.";
            mailDTO.Email = o.User.Email;

            _mailSender.SendEmail(mailDTO);

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