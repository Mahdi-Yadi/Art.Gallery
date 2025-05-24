using Art.Gallery.Core.Services.Products;
using Art.Gallery.Data.Dtos.Products;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.UserPanel.Controllers;
[Area("UserPanel")]
[Route("UserPanel/api/[controller]")]
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
        switch (result)
        {
            case ProductResult.Success:
                return Ok(new
                {
                    status = "Success"
                });
            case ProductResult.Error:
                return Ok(new
                {
                    status = "Error"
                });
            case ProductResult.Null:
                return Ok(new
                {
                    status = "Null"
                });
            case ProductResult.ImageLarge:
                return Ok(new
                {
                    status = "ImageLarge"
                });
            case ProductResult.Exist:
                return Ok(new
                {
                    status = "Exist"
                });
            case ProductResult.ImageNotUploaded:
                return Ok(new
                {
                    status = "ImageNotUploaded"
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown"
                });
        }
    }

    [HttpGet("GetProductUpdate/{id}/{userId}")]
    public IActionResult GetProductUpdate(string id,string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest(ModelState);

        var result = _productService.GetForUpdateProduct(id);

        return Ok(result);
    }

    [HttpPost("update")]
    public IActionResult UpdateProduct([FromForm] CEProductDto dto)
    {
        if (string.IsNullOrEmpty(dto.UserId))
            return BadRequest(ModelState);

        var result = _productService.UpdateProduct(dto);

        switch (result)
        {
            case ProductResult.Success:
                return Ok(new
                {
                    status = "Success"
                });
            case ProductResult.Error:
                return Ok(new
                {
                    status = "Error"
                });
            case ProductResult.Null:
                return Ok(new
                {
                    status = "Null"
                });
            case ProductResult.ImageLarge:
                return Ok(new
                {
                    status = "ImageLarge"
                });
            case ProductResult.Exist:
                return Ok(new
                {
                    status = "Exist"
                });
            case ProductResult.ImageNotUploaded:
                return Ok(new
                {
                    status = "ImageNotUploaded"
                });
            default:
                return BadRequest(new
                {
                    status = "Unknown"
                });
        }
    }

    [HttpDelete("DeleteProduct/{productId}/{userId}")]
    public IActionResult DeleteProduct(string productId, string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return BadRequest(ModelState);

        var result = _productService.DeleteProduct(productId,userId);

        switch (result)
        {
            case ProductResult.Success:
                return Ok(new
                {
                    status = "Success"
                });
            case ProductResult.Error:
                return Ok(new
                {
                    status = "Error"
                });
            case ProductResult.Null:
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
    public async Task<IActionResult> FilterProducts([FromBody] FilterProductsDto dto)
    {
        if (string.IsNullOrEmpty(dto.ArtistId) || string.IsNullOrEmpty(dto.UserId))
            return BadRequest();

        var result = await _productService.FilterProductsAsync(dto);
        
        return Ok(result);
    }
}