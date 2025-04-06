using Microsoft.AspNetCore.Mvc;

namespace Art.Gallery.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    public class HomeController : Controller
    {
        [HttpGet("panel")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
