using Art.Gallery.Core.Services.Account;
using Art.Gallery.Data.Dtos.Account;
using Art.Gallery.Data.Dtos.Products;
using Art.Gallery.Data.Entities.Account;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
[Area("AdminPanel")]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

    #region Constructor

    private readonly IAccountService _accountService;

    public UsersController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    #endregion

    #region Filter User

    [HttpPost("FilterUsersList")]
    public async Task<IActionResult> FilterUsers([FromBody] FilterUsersDto dto,string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var result = await _accountService.FilterUsers(dto);

        return Ok(result);
    }

    #endregion

    #region Check User

    [HttpGet("CheckUser/{userId}")]
    public IActionResult CheckUser(string userId)
    {
        if (userId == null)
            return BadRequest(ModelState);

        var result = _accountService.IsAdmin(userId);

        return Ok(result);
    }

    #endregion

    #region SetUserToAdmin

    [HttpGet("SetUserToAdmin/{userId}/{adminId}")]
    public IActionResult SetUserToAdmin(string userId,string adminId)
    {
        if (!_accountService.IsAdmin(adminId))
            return BadRequest();

        var result = _accountService.IsAdmin(userId);

        return Ok(result);
    }

    #endregion

    #region SetUserToNormalUser

    [HttpGet("SetUserToNormalUser/{userId}/{adminId}")]
    public IActionResult SetUserToNormalUser(string userId, string adminId)
    {
        if (!_accountService.IsAdmin(adminId))
            return BadRequest();

        var result = _accountService.IsAdmin(userId);

        return Ok(result);
    }

    #endregion

}