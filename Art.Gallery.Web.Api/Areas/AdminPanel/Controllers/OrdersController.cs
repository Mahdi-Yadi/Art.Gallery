using Art.Gallery.Core.Services.Account;
using Art.Gallery.Core.Services.Orders;
using Art.Gallery.Data.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
[Area("AdminPanel")]
[Route("AdminPanel/api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    readonly IOrderService _orderService;
    readonly IAccountService _accountService;
    public OrdersController(IOrderService orderService,
        AccountService accountService)
    {
        _orderService = orderService;
        _accountService = accountService;
    }

    [HttpPost("FilterOrdersAdmin")]
    public async Task<IActionResult> FilterOrders([FromBody] FilterOrdersDto dto)
    {
        if (!_accountService.IsAdmin(dto.UserId))
            return BadRequest();

        var result = await _orderService.FilterOrders(dto);

        return Ok(result);
    }

    [HttpPost("GetOrder/{orderId}")]
    public IActionResult FilterOrders(long orderId)
    {
        if (orderId == 0)
            return BadRequest(ModelState);

        var result = _orderService.GetOrder(orderId);

        return Ok(result);
    }

}