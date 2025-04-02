using Microsoft.AspNetCore.Mvc;

namespace Art.Gallery.Web.Areas.UserPanel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
