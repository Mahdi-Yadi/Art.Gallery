using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Api.Controllers;
public class ArtistsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}