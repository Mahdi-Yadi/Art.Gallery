using Art.Gallery.Core.Services.Account;
using Art.Gallery.Core.Services.Categories;
using Art.Gallery.Data.Dtos.Categories;
using Art.Gallery.Data.Entities.Categories;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Areas.AdminPanel.Controllers;
[Route("AdminPanel/api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ICategoryService _categoryService;

    public CategoriesController(IAccountService accountService, ICategoryService categoryService)
    {
        _accountService = accountService;
        _categoryService = categoryService;
    }

    [HttpPost("AddCategory")]
    public IActionResult AddCategory([FromBody] CategoryDto cat,string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var result = _categoryService.AddCategory(cat);
        return result switch
        {
            CatResult.Success => Ok(new
            {
                status = "Success"
            }),
            CatResult.Exist => Conflict(
                new
                {
                    status = "Exist"
                }),
            CatResult.Null => BadRequest(new
            {
                status = "name is required"
            }),
            _ => StatusCode(500, new { status = "error" })
        };
    }

    [HttpDelete("DeleteCategory/{id}")]
    public IActionResult DeleteCategory(long id,string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var result = _categoryService.DeleteCategory(id);
        return result switch
        {
            CatResult.Success => Ok(new
            {
                status = "Success"
            }),
            CatResult.Exist => Conflict(
                new
                {
                    status = "Exist"
                }),
            CatResult.Null => BadRequest(new
            {
                status = "name is required"
            }),
            _ => StatusCode(500, new { status = "error" })
        };
    }

    [HttpGet("GetCategories/{userId}")]
    public IActionResult GetCategories(string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var categories = _categoryService.GetCategories();
        return Ok(categories);
    }

    [HttpGet("GetCategory/{id}")]
    public IActionResult GetCategory(long id, string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var category = _categoryService.GetCategory(id);
        if (category == null)
            return NotFound("Category not found.");
        return Ok(category);
    }

    [HttpPut("UpdateCategory")]
    public IActionResult UpdateCategory([FromBody] CategoryDto cat,string userId)
    {
        if (!_accountService.IsAdmin(userId))
            return BadRequest();

        var result = _categoryService.UpdateCategory(cat);
        return result switch
        {
            CatResult.Success => Ok(new
            {
                status = "Success"
            }),
            CatResult.Exist => Conflict(
                new
                {
                    status = "Exist"
                }),
            CatResult.Null => BadRequest(new
            {
                status = "name is required"
            }),
            _ => StatusCode(500, new { status = "error" })
        };
    }
}