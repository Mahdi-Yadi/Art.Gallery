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

    [HttpDelete("ActiveProduct/{id}")]
    public IActionResult ActiveProduct(string id)
    {
        var result = _productService.ActiveProduct(id);
        return Ok(result);
    }

    [HttpDelete("RejectProduct/{id}")]
    public IActionResult RejectProduct(string id)
    {
        var result = _productService.RejectProduct(id);
        return Ok(result);
    }

    [HttpPost("filter")]
    public async Task<IActionResult> FilterProducts([FromBody] FilterProductsDto dto)
    {
        var result = await _productService.FilterProductsAsync(dto);
        return Ok(result);
    }

}