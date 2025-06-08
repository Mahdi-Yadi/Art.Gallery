using Art.Gallery.Core.Services.Account;
using Art.Gallery.Core.Services.SiteSettings;
using Art.Gallery.Data.Entities.Site;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
[Area("AdminPanel")]
[ApiController]
[Route("AdminPanel/api/[controller]")]
public class SiteController : ControllerBase
{

    private readonly ISiteSettingService _siteSettingService;
    private readonly IAccountService _accountService;
    public SiteController(ISiteSettingService siteSettingService,
        IAccountService accountService)
    {
        _siteSettingService = siteSettingService;
        _accountService = accountService;
    }

    [HttpGet("GetSiteSetting/{userId}")]
    public IActionResult GetSiteSetting(string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var result = _siteSettingService.GetSiteSetting();

        return Ok(result);
    }

    [HttpPost("UpdateSiteSetting/{userId}")]
    public IActionResult UpdateSiteSetting([FromForm] SiteDto dto,string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        SiteResult result = _siteSettingService.UpdateSiteSetting(dto);

        switch (result)
        {
            case SiteResult.Success:
                return Ok(new
                {
                    status = "Success"
                });
            case SiteResult.Error:
                return Ok(new
                {
                    status = "Error"
                });
            case SiteResult.ImageLarge:
                return Ok(new
                {
                    status = "ImageLarge"
                });
            case SiteResult.ImageNotUploaded:
                return Ok(new
                {
                    status = "ImageNotUploaded"
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown"
                });
        }
    }

}