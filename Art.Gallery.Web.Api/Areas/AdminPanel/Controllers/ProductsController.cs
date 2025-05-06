using Art.Gallery.Core.Services.Products;
using Art.Gallery.Data.Dtos.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
//[Authorize]
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

    [HttpPost("AddProduct")]
    public IActionResult AddProduct([FromForm] CEProductDto dto)
    {
        var result = _productService.AddProduct(dto);
        return Ok(result);
    }

    [HttpGet("GetforUpdate/{id}")]
    public IActionResult UpdateProduct(long id)
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
    public IActionResult DeleteProduct(long id)
    {
        var result = _productService.DeleteProduct(id);
        return Ok(result);
    }

    [HttpPost("filter")]
    public async Task<IActionResult> FilterProducts([FromBody] FilterProductsDto dto)
    {
        var result = await _productService.FilterProductsAsync(dto);
        return Ok(result);
    }

}