using Art.Gallery.Core.Services.Products;
using Art.Gallery.Data.Dtos.Products;
using Humanizer;
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

    [HttpGet("ActiveProduct/{productId}/{userId}")]
    public IActionResult ActiveProduct(string productId, string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest();

        var result = _productService.ActiveProduct(productId, userId);

        return Ok(result);
    }

    [HttpGet("RejectProduct/{productId}/{userId}")]
    public IActionResult RejectProduct(string productId, string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest();

        var result = _productService.RejectProduct(productId, userId);

        return Ok(result);
    }

    [HttpPost("filter")]
    public async Task<IActionResult> FilterProducts([FromBody] FilterProductsDto dto)
    {
        if (string.IsNullOrEmpty(dto.UserId))
            return BadRequest();

        var result = await _productService.FilterProductsAsync(dto);

        return Ok(result);
    }

}