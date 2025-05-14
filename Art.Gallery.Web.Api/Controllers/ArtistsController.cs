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
        dto.IsActive = true;

        var result = await _artistService.FilterArtist(dto);

        return Ok(result);
    }

    [HttpGet("Artist-Profile/{id}/{userName}")]
    public IActionResult ArtistProfile(long id, string userName)
    {
        var result = _artistService.GetArtistForShow(id.ToString(), userName);
        return Ok(result);
    }

}