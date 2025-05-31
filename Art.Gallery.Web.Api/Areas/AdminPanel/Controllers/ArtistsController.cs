using Art.Gallery.Core.Services.Account;
using Art.Gallery.Core.Services.Artists;
using Art.Gallery.Data.Dtos.Artists;
using Art.Gallery.Data.Dtos.Products;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
[Area("AdminPanel")]
[ApiController]
[Route("AdminPanel/api/[controller]")]
public class ArtistsController : ControllerBase
{
    readonly IArtistService _artistService;
    readonly IAccountService _accountService;

    public ArtistsController(IArtistService artistService, IAccountService accountService)
    {
        _artistService = artistService;
        _accountService = accountService;
    }

    [HttpGet("AcceptArtist/{artistId}/{userId}")]
    public IActionResult AcceptArtist(string artistId, string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest();

        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var result = _artistService.ActiveArtist(artistId,userId);

        switch (result)
        {
            case ArtistDtoResult.Success:
                return Ok(new
                {
                    status = "Success"
                });
            case ArtistDtoResult.Error:
                return Ok(new
                {
                    status = "Error"
                });
            case ArtistDtoResult.Null:
                return Ok(new
                {
                    status = "InvalidData"
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown"
                });
        }
    }

    [HttpGet("RejectArtist/{artistId}/{userId}")]
    public IActionResult RejectArtist(string artistId,string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest();

        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var result = _artistService.RejectArtist(artistId,userId);

        switch (result)
        {
            case ArtistDtoResult.Success:
                return Ok(new
                {
                    status = "Success"
                });
            case ArtistDtoResult.Error:
                return Ok(new
                {
                    status = "Error"
                });
            case ArtistDtoResult.Null:
                return Ok(new
                {
                    status = "InvalidData"
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown"
                });
        }
    }

    [HttpGet("FilterArtists")]
    public async Task<IActionResult> FilterArtists([FromBody] FilterArtistDto dto)
    {
        if (string.IsNullOrEmpty(dto.UserId))
            return BadRequest();

        if (!_accountService.IsAdmin(dto.UserId)) 
            return BadRequest();

        dto.UserId = "";

        var result = await _artistService.FilterArtist(dto);

        return Ok(result);
    }

}