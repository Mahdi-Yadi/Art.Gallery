using Microsoft.AspNetCore.Mvc;

namespace Art.Gallery.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HomeController : Controller
    {
        [HttpGet("System")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
