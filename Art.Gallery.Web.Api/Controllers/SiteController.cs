using Art.Gallery.Core.Services.SiteSettings;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SiteController : ControllerBase
{

    private readonly ISiteSettingService _siteSettingService;
    public SiteController(ISiteSettingService siteSettingService)
    {
        _siteSettingService = siteSettingService;
    }

    [HttpGet("GetSiteSetting")]
    public IActionResult GetSiteSetting()
    {
        var result = _siteSettingService.GetSiteSetting();

        return Ok(result);
    }

}