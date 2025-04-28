using Art.Gallery.Core.Services.Products;
using Art.Gallery.Data.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{

    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

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

    [HttpGet("Product")]
    public IActionResult Product(string slug)
    {
        return BadRequest();
    }

}