using Art.Gallery.Data.Entities.Site;
using Microsoft.AspNetCore.Http;
namespace Art.Gallery.Core.Services.SiteSettings;
public interface ISiteSettingService
{
    
    SiteSetting GetSiteSetting();

    SiteResult UpdateSiteSetting(SiteSetting siteSetting,IFormFile logoFile);

}