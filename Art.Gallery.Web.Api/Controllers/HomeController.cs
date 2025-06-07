using Art.Gallery.Core.Services.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HomeController : ControllerBase
{

    private readonly ICategoryService _categoryService;

    public HomeController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _categoryService.GetCategories();
        return Ok(categories);
    }
}