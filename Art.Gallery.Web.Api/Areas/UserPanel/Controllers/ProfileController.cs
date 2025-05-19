using Art.Gallery.Core.Services.Account;
using Art.Gallery.Data.Dtos.Account;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.UserPanel.Controllers;
[Area("UserPanel")]
[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IAccountService _accountService;

    public ProfileController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("CheckUser/{userId}")]
    public IActionResult CheckUser(string userId)
    {
        if (userId == null)
            return BadRequest(ModelState);

        var result = _accountService.IsAdmin(userId);

        return Ok(result);
    }

    [HttpGet("GetUserProfile/{userId}")]
    public IActionResult GetUserProfile(string userId)
    {
        if (userId == null)
            return BadRequest(ModelState);

        var result = _accountService.GetUserProfileForEdit(userId);

        return Ok(result);
    }

    [HttpPost("UpdateUserProfile")]
    public IActionResult UpdateUserProfile([FromForm] ProfileDto dto)
    {
        if (dto.Id == null)
            return BadRequest(ModelState);

        var result = _accountService.EditProfile(dto);
        switch (result)
        {
            case AccountResult.Success:
                return Ok(new
                {
                    status = "Success"
                });
            case AccountResult.Error:
                return Ok(new
                {
                    status = "Error"
                });
            case AccountResult.Null:
                return Ok(new
                {
                    status = "Null"
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown"
                });
        }
    }

}