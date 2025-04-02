using Microsoft.AspNetCore.Mvc;

namespace Art.Gallery.Web.Areas.UserPanel.ViewComponents;

public class UserPanelMenuViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("UserPanelMenu");
    }
}