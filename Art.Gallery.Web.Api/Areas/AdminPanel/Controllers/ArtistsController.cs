using Art.Gallery.Core.Services.Artists;
using Art.Gallery.Data.Dtos.Artists;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
//[Authorize]
[Area("AdminPanel")]
[ApiController]
[Route("AdminPanel/api/[controller]")]
public class ArtistsController : ControllerBase
{
    readonly IArtistService _artistService;

    public ArtistsController(IArtistService artistService)
    {
        _artistService = artistService;        
    }

    [HttpDelete("AcceptArtist/{id}")]
    public IActionResult AcceptArtist(long id)
    {
        var result = _artistService.ActiveArtist(id.ToString());
        return Ok(result);
    }

    [HttpDelete("RejectArtist/{id}")]
    public IActionResult RejectArtist(long id)
    {
        var result = _artistService.RejectArtist(id.ToString());
        return Ok(result);
    }

    [HttpPost("filter")]
    public async Task<IActionResult> FilterArtists([FromBody] FilterArtistDto dto)
    {
        var result = await _artistService.FilterArtist(dto);
        return Ok(result);
    }

}