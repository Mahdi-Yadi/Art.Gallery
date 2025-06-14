using Art.Gallery.Core.Services.Account;
using Art.Gallery.Core.Services.Orders;
using Art.Gallery.Data.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
[Area("AdminPanel")]
[ApiController]
[Route("AdminPanel/api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IAccountService _accountService;

    public OrdersController(IOrderService orderService,
        IAccountService accountService)
    {
        _orderService = orderService;
        _accountService = accountService;
    }

    [HttpPost("FilterOrdersAdmin")]
    public async Task<IActionResult> FilterOrders([FromBody] FilterOrdersDto dto)
    {
        if (!_accountService.IsAdmin(dto.UserId))
            return BadRequest();

        FilterOrdersDto result = await _orderService.FilterOrders(dto);

        return Ok(result);
    }

    [HttpPost("Get-Order/{userId}/{orderId}")]
    public IActionResult GetOrder(string userId,long orderId)
    {
        if (orderId == 0)
            return BadRequest(ModelState);

        if (!_accountService.IsAdmin(userId))
            return BadRequest();


        OrderDto result = _orderService.GetOrder(orderId);

        return Ok(result);
    }

    [HttpPost("UpdateOrderForNotComplete/{orderId}/{userId}")]
    public IActionResult UpdateOrderForNotComplete(long orderId, string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        bool result = _orderService.UpdateOrderForNotComplete(orderId);

        return Ok(result);
    }

    [HttpPost("UpdateOrderForComplete/{orderId}/{userId}")]
    public IActionResult UpdateOrderForComplete(long orderId, string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        bool result = _orderService.UpdateOrderForComplete(orderId);

        return Ok(result);
    }

}