using Art.Gallery.Core.Services.Account;
using Art.Gallery.Data.Dtos.Account;
using Art.Gallery.Data.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
[Area("AdminPanel")]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    private readonly IAccountService _accountService;

    public UsersController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("FilterUsers")]
    public async Task<IActionResult> FilterUsers([FromBody] FilterUsersDto dto)
    {
        var result = await _accountService.FilterUsers(dto);
        return Ok(result);
    }

    [HttpGet("CheckUser/{userId}")]
    public IActionResult CheckUser(string userId)
    {
        if (userId == null)
            return BadRequest(ModelState);

        var result = _accountService.IsAdmin(userId);

        return Ok(result);
    }

}