using Microsoft.AspNetCore.Mvc;
namespace Art.Gallery.Web.Areas.AdminPanel.ViewComponents;
public class AdminPanelMenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("AdminPanelMenu");
    }
}