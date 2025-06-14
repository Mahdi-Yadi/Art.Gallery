﻿using Art.Gallery.Core.Services.Artists;
using Art.Gallery.Data.Dtos.Artists;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.UserPanel.Controllers;
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

    [HttpPost("AddArtist")]
    public IActionResult AddArtist([FromForm] CEArtistDto dto)
    {
        if (dto.UserId == null)
            return BadRequest(ModelState);

        ArtistDtoResult result = _artistService.AddArtist(dto);

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

    [HttpGet("GetForUpdate/{artistId}/{userId}")]
    public IActionResult UpdateArtist(string artistId, string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest(ModelState);

        var result = _artistService.GetArtist(artistId,userId);
        return Ok(result);
    }

    [HttpPost("update")]
    public IActionResult UpdateArtist([FromForm] CEArtistDto dto)
    {
        if (string.IsNullOrEmpty(dto.UserId))
            return BadRequest(ModelState);

        var result = _artistService.UpdateArtist(dto);
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

    [HttpDelete("DeleteArtist/{artistId}/{userId}")]
    public IActionResult DeleteArtist(string artistId, string userId)
    {
        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(artistId))
            return BadRequest(ModelState);

        var result = _artistService.DeleteArtist(artistId, userId);
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
                    status = "Null"
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown"
                });
        }
    }

    [HttpPost("Filter")]
    public async Task<IActionResult> FilterArtists([FromBody] FilterArtistDto dto)
    {
        if (string.IsNullOrEmpty(dto.UserId))
            return BadRequest(ModelState);

        var result = await _artistService.FilterArtist(dto);

        return Ok(result);
    }

}