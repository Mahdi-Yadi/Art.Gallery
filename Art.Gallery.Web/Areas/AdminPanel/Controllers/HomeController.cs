using Microsoft.AspNetCore.Mvc;

namespace Art.Gallery.Web.Areas.AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
