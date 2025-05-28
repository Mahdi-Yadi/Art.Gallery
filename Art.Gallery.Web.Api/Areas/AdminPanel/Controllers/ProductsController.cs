using Art.Gallery.Core.Services.Products;
using Art.Gallery.Data.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
[Area("AdminPanel")]
[ApiController]
[Route("AdminPanel/api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly IProductsService _productService;

    public ProductsController(IProductsService productService)
    {
        _productService = productService;
    }

    [HttpGet("ActiveProduct/{productId}/{artistId}")]
    public IActionResult ActiveProduct(string productId, string artistId)
    {
        if (string.IsNullOrEmpty(artistId))
            return BadRequest();

        var result = _productService.ActiveProduct(productId, artistId);

        return Ok(result);
    }

    [HttpGet("RejectProduct/{productId}/{artistId}")]
    public IActionResult RejectProduct(string productId, string artistId)
    {
        if (string.IsNullOrEmpty(artistId))
            return BadRequest();

        var result = _productService.RejectProduct(productId, artistId);

        return Ok(result);
    }

    [HttpPost("filter")]
    public async Task<IActionResult> FilterProducts([FromBody] FilterProductsDto dto)
    {
        if (string.IsNullOrEmpty(dto.ArtistId))
            return BadRequest();

        var result = await _productService.FilterProductsAsync(dto);

        return Ok(result);
    }

}