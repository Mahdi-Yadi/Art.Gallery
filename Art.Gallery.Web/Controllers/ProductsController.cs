using Art.Gallery.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Controllers;
/// <summary>
/// www.technoto.org/api/Products/GetProducts
/// https://localhost:44350/api/Products/GetProducts
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = new List<object>
        {
            new { Id = 1, Name = "Product 1", Price = 100 },
            new { Id = 2, Name = "Product 2", Price = 200 },
            new { Id = 3, Name = "Product 3", Price = 300 }
        };
       
        return Ok(products);
    }
}