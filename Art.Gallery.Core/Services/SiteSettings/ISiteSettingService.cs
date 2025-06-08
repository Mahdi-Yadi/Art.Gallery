using Art.Gallery.Data.Entities.Site;
namespace Art.Gallery.Core.Services.SiteSettings;
public interface ISiteSettingService
{

    SiteDto GetSiteSetting();

    SiteResult UpdateSiteSetting(SiteDto dto);

}