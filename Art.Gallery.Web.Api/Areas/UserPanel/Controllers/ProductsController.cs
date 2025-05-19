using Art.Gallery.Core.Services.Products;
using Art.Gallery.Data.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.UserPanel.Controllers;
[Area("UserPanel")]
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{

    private readonly IProductsService _productService;

    public ProductsController(IProductsService productService)
    {
        _productService = productService;
    }

    [HttpPost("AddProduct")]
    public IActionResult AddProduct([FromForm] CEProductDto dto)
    {
        if (string.IsNullOrEmpty(dto.ArtistId) || string.IsNullOrEmpty(dto.UserId))
            return BadRequest();

        var result = _productService.AddProduct(dto);
        return Ok(result);
    }

    [HttpGet("GetProductUpdate/{id}")]
    public IActionResult GetProductUpdate(string id)
    {
        var result = _productService.GetForUpdateProduct(id);

        return Ok(result);
    }

    [HttpPost("update")]
    public IActionResult UpdateProduct([FromForm] CEProductDto dto)
    {
        var result = _productService.UpdateProduct(dto);

        return Ok(result);
    }

    [HttpDelete("DeleteProduct/{id}")]
    public IActionResult DeleteProduct(string id)
    {
        var result = _productService.DeleteProduct(id);

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