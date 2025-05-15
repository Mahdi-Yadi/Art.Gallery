using Art.Gallery.Core.Services.Artists;
using Art.Gallery.Data.Dtos.Artists;
using Art.Gallery.Data.Entities.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.UserPanel.Controllers;
//[Authorize]
[Area("UserPanel")]
[ApiController]
[Route("UserPanel/api/[controller]")]
public class ArtistsController : ControllerBase
{
    readonly IArtistService _artistService;

    public ArtistsController(IArtistService artistService)
    {
        _artistService = artistService;        
    }
    // تست
    [Authorize]
    [HttpGet("test")]
    public IActionResult GetSecret() => Ok("مجاز!");
   
    [HttpPost("AddArtist")]
    public IActionResult AddArtist([FromForm] CEArtistDto dto)
    {
        if (dto.UserId == null)
            return BadRequest(ModelState);

        var result = _artistService.AddArtist(dto);
        return Ok(result);
    }

    [HttpGet("GetforUpdate/{id}/{userId}")]
    public IActionResult UpdateArtist(long id,string userId)
    {
        if (userId == null)
            return BadRequest(ModelState);

        var result = _artistService.GetArtist(id.ToString(),userId);
        return Ok(result);
    }

    [HttpPost("update")]
    public IActionResult UpdateArtist([FromForm] CEArtistDto dto)
    {
        if (dto.UserId == null)
            return BadRequest(ModelState);

        var result = _artistService.UpdateArtist(dto);
        return Ok(result);
    }

    [HttpDelete("DeleteArtist/{id}/{userId}")]
    public IActionResult DeleteArtist(long id, string userId)
    {
        if (userId == null)
            return BadRequest(ModelState);

        var result = _artistService.DeleteArtist(id.ToString(), userId);
        return Ok(result);
    }

    [HttpPost("filter")]
    public async Task<IActionResult> FilterArtists([FromBody] FilterArtistDto dto)
    {
        if (dto.UserId == null)
            return BadRequest(ModelState);

        var result = await _artistService.FilterArtist(dto);
        return Ok(result);
    }

}