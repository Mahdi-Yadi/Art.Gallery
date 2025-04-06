using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Controllers;
public class AccountController : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Forgot()
    {
        return View();
    }

    public IActionResult Reset()
    {
        return View();
    }
}