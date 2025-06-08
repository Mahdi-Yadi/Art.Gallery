using Art.Gallery.Common;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Products;
using Art.Gallery.Data.Entities.Site;
using Microsoft.AspNetCore.Http;
namespace Art.Gallery.Core.Services.SiteSettings;
public class SiteSettingService : ISiteSettingService
{

    private readonly SiteDBContext _db;
    private readonly UrlProtector _urlProtector;

    public SiteSettingService(SiteDBContext db, UrlProtector urlProtector)
    {
        _db = db;
        _urlProtector = urlProtector;
    }

    public SiteDto GetSiteSetting()
    {

        var siteSetting = _db.SiteSettings.FirstOrDefault(a => !a.IsDelete);

        if (siteSetting == null)
        {
            SiteSetting site = new SiteSetting();

            site.Title = "عنوان سایت";
            site.IsDelete = false;
            site.Address = "ایران";
            site.Email = "info@technoto.org";
            site.CreateDate = DateTime.Now;
            site.UpdateDate = DateTime.Now;
            site.Phone = "0";
            site.Name = "نام وب سایت";
            site.ImageName = "logoName.png";

            _db.SiteSettings.Add(site);
            _db.SaveChanges();

            siteSetting = site;
        }

        SiteDto dto = new SiteDto();

        dto.Id = _urlProtector.Protect(siteSetting.Id.ToString());
        dto.Title = siteSetting.Title;
        dto.Address = siteSetting.Address;
        dto.Email = siteSetting.Email;
        dto.Phone = siteSetting.Phone;
        dto.Text = siteSetting.Text;
        dto.Name = siteSetting.Name;
        dto.ImageName = PathExtension.DomainAddress + PathExtension.SiteImage + siteSetting.ImageName;

        return dto;
    }

    public SiteResult UpdateSiteSetting(SiteDto dto)
    {
        try
        {
            long siteId = Convert.ToInt64(_urlProtector.UnProtect(dto.Id));

            var site = _db.SiteSettings.FirstOrDefault(a => a.Id == siteId);

            if (site == null)
            {
                GetSiteSetting();
                site = _db.SiteSettings.FirstOrDefault(a => !a.IsDelete);
            }

            if (dto.ImageFile != null)
            {
                if (dto.ImageFile.Length > 10100000)
                {
                    return SiteResult.ImageLarge;
                }

                var imageName = TextFixer.FixTextForUrl(dto.Name) + Path.GetExtension(dto.ImageFile.FileName);

                var res = dto.ImageFile.AddImageToServer(imageName, PathExtension.SiteImageServer,
                    null, null, null, null);

                if (!res)
                {
                    return SiteResult.ImageNotUploaded;
                }

                site.ImageName = imageName;
            }

            site.Text = dto.Text;
            site.Address = dto.Address;
            site.Title = dto.Title;
            site.Email = dto.Email;
            site.Phone = dto.Phone;
            site.Name = dto.Name;

            _db.SiteSettings.Update(site);
            _db.SaveChanges();

            return SiteResult.Success;
        }
        catch (Exception)
        {
            return SiteResult.Error;
        }
    }

}
public enum SiteResult
{
    ImageLarge,
    ImageNotUploaded,
    Success,
    Error
}