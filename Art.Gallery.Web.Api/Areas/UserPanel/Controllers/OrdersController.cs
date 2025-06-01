using Art.Gallery.Core.Services.Orders;
using Art.Gallery.Data.Dtos.Artists;
using Art.Gallery.Data.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;
using Parbad;
using Parbad.AspNetCore;
using Parbad.Gateway.ZarinPal;
namespace Art.Gallery.Web.Api.Areas.UserPanel.Controllers;
[Area("UserPanel")]
[Route("UserPanel/api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    readonly IOrderService _orderService;
    private readonly IOnlinePayment _onlinePayment;

    public OrdersController(IOrderService orderService, IOnlinePayment onlinePayment)
    {
        _orderService = orderService;
        _onlinePayment = onlinePayment;
    }

    [HttpPost("AddProductToOrder")]
    public IActionResult AddProductToOrder(long productId, string userId)
    {
        if (string.IsNullOrEmpty(userId) || productId == 0)
            return BadRequest(ModelState);

        OrderResult result = _orderService.AddOrder(productId, userId);

        switch (result)
        {
            case OrderResult.Success:
                return Ok(new
                {
                    status = "Success"
                });
            case OrderResult.Error:
                return Ok(new
                {
                    status = "Error"
                });
            case OrderResult.Nulls:
                return Ok(new
                {
                    status = "InvalidData"
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown"
                });
        }
    }

    [HttpPost("DeleteProductFromOrder")]
    public IActionResult DeleteProductFromOrder(long productId, string userId)
    {
        if (string.IsNullOrEmpty(userId) || productId == 0)
            return BadRequest(ModelState);

        OrderResult result = _orderService.DeleteProductFromOrder(productId, userId);

        switch (result)
        {
            case OrderResult.Success:
                return Ok(new
                {
                    status = "Success"
                });
            case OrderResult.Error:
                return Ok(new
                {
                    status = "Error"
                });
            case OrderResult.Nulls:
                return Ok(new
                {
                    status = "InvalidData"
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown"
                });
        }
    }

    [HttpPost("GetOrder/{orderId}")]
    public IActionResult FilterOrders(long orderId)
    {
        if (orderId == 0)
            return BadRequest(ModelState);

        var result = _orderService.GetOrder(orderId);

        return Ok(result);
    }

    [HttpPost("UserFilterOrders")]
    public async Task<IActionResult> FilterOrders([FromBody] FilterOrdersDto dto)
    {
        if (string.IsNullOrEmpty(dto.UserId))
            return BadRequest(ModelState);

        var result = await _orderService.FilterOrders(dto);

        return Ok(result);
    }

    [HttpGet("Pay/{userId}")]
    public async Task<IActionResult> Pay(string userId)
    {
        var order = _orderService.GetOpenOrder(userId);

        if (order == null || order.OrderDetails.Count == 0)
            return BadRequest();

        decimal totalPrice = 0;

        foreach (var item in order.OrderDetails)
        {
            decimal price = (decimal)item.Product.Price * item.Count;

            totalPrice += price;
        }

        try
        {
            string trackingCode = $"{DateTime.Now:yyyyMMddHHmmmmssffff}";

            string callback = "https://localhost:44331/VerifyPay";

            totalPrice *= 10;

            var res = await _onlinePayment.RequestAsync(invoice =>
            {
                invoice.SetZarinPalData("خرید از وب سایت گالری هنر")
                    .SetTrackingNumber(Convert.ToInt64(trackingCode))
                    .SetAmount(totalPrice)
                    .SetCallbackUrl(callback)
                    .SetGateway("ZarinPal");
            });

            var resOrder = _orderService.UpdateOrderForPayment(order.Id, trackingCode);
            if (!resOrder)
            {
                resOrder = _orderService.UpdateOrderForPayment(order.Id, trackingCode);
                if (!resOrder)
                    return BadRequest();
            }

            if (res.IsSucceed)
                return res.GatewayTransporter.TransportToGateway();

        }
        catch (Exception)
        {
            return BadRequest();
        }

        return BadRequest();
    }

    [HttpGet("VerifyPay")]
    [HttpPost("VerifyPay")]
    public async Task<IActionResult> VerifyPay()
    {
        var invoice = await _onlinePayment.FetchAsync();

        if (invoice == null)
        {
            return BadRequest();
        }

        if (invoice.Status != PaymentFetchResultStatus.ReadyForVerifying)
        {
            return BadRequest();
        }

        var verifyResult = await _onlinePayment.VerifyAsync(invoice);

        if (verifyResult.Status == PaymentVerifyResultStatus.Succeed)
        {
            var res = _orderService.UpdateOrderAfterPayment(verifyResult.TrackingNumber.ToString(), verifyResult.TransactionCode);

            if (res)
            {
                return Ok(new
                {
                    status = "Success",
                    code = verifyResult.TransactionCode
                });
            }
            else
            {
                return Ok(new
                {
                    status = "Success",
                    code = "پرداخت انجام شد، اما خطای سیستم داریم.!!!"
                });
            }
        }
        else
        {
            return Ok(new
            {
                status = "Error",
                code = "پرداخت انجام نشد!"
            });
        }

    }

}