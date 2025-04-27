using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Controllers;
public class ProductsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}