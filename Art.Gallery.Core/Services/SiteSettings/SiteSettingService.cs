using Art.Gallery.Common;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Products;
using Art.Gallery.Data.Entities.Site;
using Microsoft.AspNetCore.Http;
namespace Art.Gallery.Core.Services.SiteSettings;
public class SiteSettingService : ISiteSettingService
{

    private readonly SiteDBContext _db;

    public SiteSettingService(SiteDBContext siteDB)
    {
        _db = siteDB;
    }

    public SiteSetting GetSiteSetting()
    {

        var siteSetting = _db.SiteSettings.FirstOrDefault(a => a.IsDelete == false);

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

        siteSetting.ImageName = PathExtension.DomainAddress + PathExtension.SiteImage + siteSetting.ImageName;

        return siteSetting;
    }

    public SiteResult UpdateSiteSetting(SiteSetting siteSetting, IFormFile logoFile)
    {
        try
        {
            var site = _db.SiteSettings.FirstOrDefault(a => a.IsDelete == false);

            if (site == null)
            {
                site = GetSiteSetting();
            }

            if(logoFile != null)
            {
                if (logoFile.Length > 10100000)
                {
                    return SiteResult.ImageLarge;
                }

                var imageName = TextFixer.FixTextForUrl(site.Name) + Path.GetExtension(logoFile.FileName);

                var res = logoFile.AddImageToServer(imageName, PathExtension.SiteImageServer,
                    null, null, null, null);

                if (!res)
                {
                    return SiteResult.ImageNotUploaded;
                }

                site.ImageName = imageName;
            }

            site.Text = siteSetting.Text;
            site.Address = siteSetting.Address;
            site.Title = siteSetting.Title;
            site.Email = siteSetting.Email;
            site.Phone = siteSetting.Phone;
            site.Name = siteSetting.Name;

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