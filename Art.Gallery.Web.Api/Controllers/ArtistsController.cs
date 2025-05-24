using Art.Gallery.Core.Services.Artists;
using Art.Gallery.Data.Dtos.Artists;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ArtistsController : ControllerBase
{
    readonly IArtistService _artistService;

    public ArtistsController(IArtistService artistService)
    {
        _artistService = artistService;
    }


    [HttpPost("FilterArtists")]
    public async Task<IActionResult> FilterArtists([FromBody] FilterArtistDto dto)
    {
        if (!string.IsNullOrEmpty(dto.UserId))
            dto.UserId = "";

        dto.IsActive = true;

        var result = await _artistService.FilterArtist(dto);

        return Ok(result);
    }

    [HttpGet("Artist-Profile/{userName}")]
    public IActionResult ArtistProfile(string userName)
    {
        var result = _artistService.GetArtistForShow(userName);

        if (result == null)
            return null;

        return Ok(result);
    }

}