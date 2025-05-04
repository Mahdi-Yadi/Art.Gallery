using Art.Gallery.Common;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Artists;
using Art.Gallery.Data.Dtos.Products;
using Art.Gallery.Data.Entities.Artists;
using Art.Gallery.Data.Entities.Products;
using Ganss.Xss;
using System;
namespace Art.Gallery.Core.Services.Artists;
public class ArtistService : IArtistService
{

    private SiteDBContext _db;

    public ArtistService(SiteDBContext db)
    {
        _db = db;
    }

    public ArtistDtoResult AddArtist(CEArtistDto dto)
    {
        HtmlSanitizer san = new HtmlSanitizer();
        Artist a = new Artist();

        a.Name = san.Sanitize(dto.Name);
        a.Description = san.Sanitize(dto.Description);
        a.Slug = san.Sanitize(dto.Slug);

        if (dto.ImageFile != null)
        {
            if (dto.ImageFile.Length > 10100000)
            {
                return ArtistDtoResult.ImageLarge;
            }

            DateTime curentTime = DateTime.Now;

            var newName = a.Name + "-" + curentTime.ToString("ssmmMMddHHyyyy");

            var imageName = TextFixer.FixTextForUrl(newName) + Path.GetExtension(dto.ImageFile.FileName);

            var res = dto.ImageFile.AddImageToServer(imageName, PathExtension.ProductImageServer,
                300, 300, PathExtension.ProductImageThumbServer, null);

            if (!res)
            {
                return ArtistDtoResult.ImageNotUploaded;
            }
            a.ImageName = imageName;
        }

        try
        {
            _db.Artists.Add(a);
            _db.SaveChanges();
            return ArtistDtoResult.Success;
        }
        catch (Exception)
        {
            return ArtistDtoResult.Error;
        }
    }

    public ArtistDtoResult DeleteArtist(string id)
    {
        var a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(id));

        if (a != null)
        {
            _db.Artists.Remove(a);
            _db.SaveChanges();
            return ArtistDtoResult.Success;
        }
        return ArtistDtoResult.Error;
    }

    public FilterArtistDto FilterArtist(FilterArtistDto dto)
    {
        throw new NotImplementedException();
    }

    public CEArtistDto GetArtist(string id)
    {
        Artist a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(id));

        if (a == null)
            return null;

        CEArtistDto dto = new CEArtistDto();

        dto.Name = a.Name;
        dto.Description = a.Description;
        dto.Slug = a.Slug;
        dto.ImageName = a.ImageName;

        return dto;
    }

    public ArtistDtoResult UpdateArtist(CEArtistDto dto)
    {
        HtmlSanitizer san = new HtmlSanitizer();

        Artist a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(dto.Id));

        a.Name = san.Sanitize(dto.Name);
        a.Description = san.Sanitize(dto.Description);
        a.Slug = san.Sanitize(dto.Slug);

        try
        {
            _db.Artists.Update(a);
            _db.SaveChanges();
            return ArtistDtoResult.Success;
        }
        catch (Exception)
        {
            return ArtistDtoResult.Error;
        }
    }
}