using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    
}