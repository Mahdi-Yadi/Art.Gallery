using Art.Gallery.Core.Services.Products;
using Art.Gallery.Data.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    // لیست کل محصولات
    [HttpGet("Products")]
    public async Task<ActionResult<FilterProductsDto>> Products([FromQuery] FilterProductsDto dto)
    {
        var result = await _productsService.FilterProductsAsync(dto);

        if (dto.PageId > result.PageCount && result.PageCount != 0)
        {
            return BadRequest($"شماره صفحه {dto.PageId} خارج از محدوده است. حداکثر صفحه {result.PageCount}.");
        }

        return Ok(result);
    }

     // محصول
    [HttpGet("Product")]
    public async Task<IActionResult> Product(string slug)
    {
        var result = await _productsService.GetProduct(slug);

        if (result == null)
            return null;

        return Ok(result);
    }

    // اخرین محصولات 
    [HttpGet("LastProducts")]
    public IActionResult LastProducts()
    {
        var result =  _productsService.GetLastProducts();

        if (result == null)
            return null;

        return Ok(result);
    }

    // محصولات ویژه
    [HttpGet("SpecialProducts")]
    public IActionResult SpecialProducts()
    {
        var result = _productsService.GetSpecialProducts();

        if (result == null)
            return null;

        return Ok(result);
    }

}