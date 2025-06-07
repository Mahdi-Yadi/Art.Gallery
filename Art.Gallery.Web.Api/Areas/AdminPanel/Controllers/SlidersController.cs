using Art.Gallery.Core.Services.Account;
using Art.Gallery.Core.Services.Sliders;
using Art.Gallery.Data.Entities.Account;
using Art.Gallery.Data.Entities.Site;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
[Route("api/[controller]")]
[ApiController]
public class SlidersController : ControllerBase
{
    private readonly ISliderService _sliderService;
    private readonly IAccountService _accountService;

    public SlidersController(ISliderService sliderService, IAccountService accountService)
    {
        _sliderService = sliderService;
        _accountService = accountService;
    }

    [HttpPost("AddSlider")]
    public IActionResult AddSlider([FromBody] Slider slider, string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var result = _sliderService.AddSlider(slider);
        return result switch
        {
            SliderResult.Success => Ok("success"),
            SliderResult.Null => BadRequest("Title is required."),
            _ => StatusCode(500, "An error occurred while adding the slider.")
        };
    }

    [HttpPut("UpdateSlider")]
    public IActionResult UpdateSlider([FromBody] Slider slider, string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var result = _sliderService.UpdateSlider(slider);
        return result switch
        {
            SliderResult.Success => Ok("success"),
            SliderResult.Null => NotFound("Slider not found."),
            _ => StatusCode(500, "An error occurred while updating the slider.")
        };
    }

    [HttpDelete("DeleteSlider/id")]
    public IActionResult DeleteSlider(long id,string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var result = _sliderService.DeleteSlider(id);
        return result switch
        {
            SliderResult.Success => Ok("success"),
            SliderResult.Null => NotFound("Slider not found."),
            _ => StatusCode(500, "An error occurred while deleting the slider.")
        };
    }

    [HttpGet("GetSlider/{id}")]
    public IActionResult GetSlider(long id, string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var slider = _sliderService.GetSlider(id);
        if (slider == null)
            return NotFound("Slider not found.");
        return Ok(slider);
    }

    [HttpGet("GetSliders")]
    public IActionResult GetSliders(string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var sliders = _sliderService.GetSliders();
        return Ok(sliders);
    }
}