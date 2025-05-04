using Art.Gallery.Core.Services.Products;
using Art.Gallery.Data.Dtos.Products;
using Art.Gallery.Data.Entities.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AdminProductController : ControllerBase
{
    private readonly IProductsService _productService;

    public AdminProductController(IProductsService productService)
    {
        _productService = productService;
    }

    [HttpPost("AddProduct")]
    public IActionResult AddProduct([FromForm] CEProductDto dto)
    {
        var result = _productService.AddProduct(dto);
        return Ok(result);
    }
    //[HttpPost("update")]
    //public IActionResult UpdateProduct([FromForm] Product product, [FromForm] IFormFile imageFile)
    //{
    //    var result = _productService.UpdateProduct(product, imageFile);
    //    return Ok(result);
    //}

    //[HttpDelete("{id}")]
    //public IActionResult DeleteProduct(long id)
    //{
    //    var result = _productService.DeleteProduct(id);
    //    return Ok(result);
    //}

    //[HttpGet("get-for-update/{id}")]
    //public IActionResult GetForUpdateProduct(long id)
    //{
    //    var product = _productService.GetForUpdateProduct(id);
    //    return Ok(product);
    //}

    //[HttpGet("get-by-slug/{slug}")]
    //public async Task<IActionResult> GetProduct(string slug)
    //{
    //    var product = await _productService.GetProduct(slug);
    //    return Ok(product);
    //}

    //[HttpPost("filter")]
    //public async Task<IActionResult> FilterProducts([FromBody] FilterProductsDto dto)
    //{
    //    var result = await _productService.FilterProductsAsync(dto);
    //    return Ok(result);
    //}


}