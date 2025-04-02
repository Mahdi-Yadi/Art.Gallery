using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Art.Gallery.Web.ViewComponents;

#region Meta Tags

public class MetaTagsViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("MetaTags");
    }
}

#endregion

#region Site Header

public class SiteHeaderViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("SiteHeader");
    }
}

#endregion

#region Site Footer

public class SiteFooterViewComponent : ViewComponent
{

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("SiteFooter");
    }
}

#endregion