using Art.Gallery.Core.Services.Orders;
using Art.Gallery.Data.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
[Area("s")]
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    readonly IOrderService _orderService;
   
    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("FilterOrdersAdmin")]
    public async Task<IActionResult> FilterOrders([FromBody] FilterOrdersDto dto)
    {
        if (string.IsNullOrEmpty(dto.UserId))
            return BadRequest();

        var result = await _orderService.FilterOrders(dto);
        return Ok(result);
    }

}