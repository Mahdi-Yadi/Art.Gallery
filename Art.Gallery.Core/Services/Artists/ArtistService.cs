using Art.Gallery.Common;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Artists;
using Art.Gallery.Data.Dtos.Paging;
using Art.Gallery.Data.Entities.Artists;
using Ganss.Xss;
using Microsoft.EntityFrameworkCore;
namespace Art.Gallery.Core.Services.Artists;
public class ArtistService : IArtistService
{

    private SiteDBContext _db;
    private readonly UrlProtector _urlProtector;
    public ArtistService(SiteDBContext db, UrlProtector urlProtector)
    {
        _db = db;
        _urlProtector = urlProtector;
    }

    // Add 
    public ArtistDtoResult AddArtist(CEArtistDto dto)
    {
        HtmlSanitizer san = new HtmlSanitizer();
        Artist a = new Artist();

        DateTime curentTime = DateTime.Now;

        a.Name = san.Sanitize(dto.Name);
        a.Description = san.Sanitize(dto.Description);
        a.Slug = curentTime.ToString("yyyyMMddHHmmmmss") + "-" + san.Sanitize(dto.Slug).Replace(" ", "-");
        a.UserId = Convert.ToInt64(_urlProtector.UnProtect(dto.UserId));

        if (dto.ImageFile != null)
        {
            if (dto.ImageFile.Length > 10100000)
            {
                return ArtistDtoResult.ImageLarge;
            }

            var newName = a.Name + "-" + curentTime.ToString("ssmmMMddHHyyyy");

            var imageName = TextFixer.FixTextForUrl(newName) + Path.GetExtension(dto.ImageFile.FileName);

            var res = dto.ImageFile.AddImageToServer(imageName, PathExtension.ArtistImageServer,
                300, 300, PathExtension.ArtistImageThumbServer, null);

            if (!res)
            {
                return ArtistDtoResult.ImageNotUploaded;
            }
            a.ImageName = imageName;
        }
        else
        {
            a.ImageName = "1.png";
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

    // Delete
    public ArtistDtoResult DeleteArtist(string artistId, string userId)
    {
        var a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(_urlProtector.UnProtect(artistId)) &&
        a.UserId == Convert.ToInt64(_urlProtector.UnProtect(userId)));

        if (a != null)
        {
            a.IsDelete = true;

            _db.Artists.Update(a);
            _db.SaveChanges();
            return ArtistDtoResult.Success;
        }
        return ArtistDtoResult.Error;
    }

    // Active
    public ArtistDtoResult ActiveArtist(string artistId, string userId)
    {
        try
        {
            var a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(_urlProtector.UnProtect(artistId))
        && a.UserId == Convert.ToInt64(_urlProtector.UnProtect(userId)));

            if (a != null)
            {
                a.IsActive = true;

                _db.Artists.Update(a);
                _db.SaveChanges();
                return ArtistDtoResult.Success;
            }
            return ArtistDtoResult.Null;
        }
        catch (Exception)
        {
            return ArtistDtoResult.Error;
        }
    }

    // Reject
    public ArtistDtoResult RejectArtist(string artistId, string userId)
    {
        try
        {
            var a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(_urlProtector.UnProtect(artistId))
        && a.UserId == Convert.ToInt64(_urlProtector.UnProtect(userId)));

            if (a != null)
            {
                a.IsActive = false;

                _db.Artists.Update(a);
                _db.SaveChanges();
                return ArtistDtoResult.Success;
            }
            return ArtistDtoResult.Null;
        }
        catch (Exception)
        {
            return ArtistDtoResult.Error;
        }
    }

    // Recover
    public ArtistDtoResult RecoverArtist(string artistId, string userId)
    {
        var a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(_urlProtector.UnProtect(artistId)) &&
        a.UserId == Convert.ToInt64(_urlProtector.UnProtect(userId)));

        if (a != null)
        {
            a.IsDelete = false;

            _db.Artists.Update(a);
            _db.SaveChanges();
            return ArtistDtoResult.Success;
        }

        return ArtistDtoResult.Error;
    }

    // Filter
    public async Task<FilterArtistDto> FilterArtist(FilterArtistDto dto)
    {
        var query = _db.Artists.AsQueryable();

        if (!string.IsNullOrEmpty(dto.UserId))
            query = query.Where(a => a.UserId == Convert.ToInt64(_urlProtector.UnProtect(dto.UserId)));

        var aQuery = query.Select(p => new
        {
            p.Id,
            p.Name,
            p.ImageName,
            p.Slug,
            p.IsDelete,
            p.IsActive,
            p.UserId
        });

        if (!string.IsNullOrEmpty(dto.UserId))
            query = query.Where(a => a.UserId == Convert.ToInt64(_urlProtector.UnProtect(dto.UserId)));

        if (!string.IsNullOrEmpty(dto.Name))
            query = query.Where(a => a.Name == dto.Name);

        var products = (await aQuery.ToListAsync()).Select(p => new ArtistDto()
        {
            ArtistId = _urlProtector.Protect(p.Id.ToString()),
            UserId = _urlProtector.Protect(p.UserId.ToString()),
            Name = p.Name,
            ImageName = PathExtension.DomainAddress +
                PathExtension.ArtistImage + p.ImageName,
            Slug = p.Slug,
            IsDeleted = p.IsDelete,
            IsActive = p.IsActive
        }).ToList();

        #region Pagination

        var pager = Pager.Build(dto.PageId, await query.CountAsync(), dto.TakeEntity, dto.HowManyShowPageAfterAndBefore);

        dto.Count = products.Count;

        return dto.SetArtists(products).SetPaging(pager);

        #endregion

    }

    // For show
    public CEArtistDto GetArtistForShow(string userName)
    {
        Artist a = _db.Artists.FirstOrDefault(a =>
                a.IsActive && a.Slug == userName);

        if (a == null)
            return null;

        CEArtistDto dto = new CEArtistDto();

        dto.Name = a.Name;
        dto.Description = a.Description;
        dto.Slug = a.Slug;
        dto.ImageName = PathExtension.DomainAddress +
                PathExtension.ArtistImage + a.ImageName;

        return dto;
    }

    // Get data
    public CEArtistDto GetArtist(string artistId, string userId)
    {
        Artist a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(_urlProtector.UnProtect(artistId)) &&
        a.UserId == Convert.ToInt64(_urlProtector.UnProtect(userId)));

        if (a == null)
            return null;

        CEArtistDto dto = new CEArtistDto();

        dto.ArtistId = artistId;
        dto.Name = a.Name;
        dto.Description = a.Description;
        dto.Slug = a.Slug;
        dto.ImageName = PathExtension.DomainAddress +
                PathExtension.ArtistImage + a.ImageName;

        return dto;
    }

    // Update 
    public ArtistDtoResult UpdateArtist(CEArtistDto dto)
    {
        HtmlSanitizer san = new HtmlSanitizer();

        Artist a = _db.Artists.FirstOrDefault(a => a.Id ==
                                                   Convert.ToInt64(_urlProtector.UnProtect(dto.ArtistId)));

        DateTime curentTime = DateTime.Now;

        a.Name = san.Sanitize(dto.Name);
        a.Description = san.Sanitize(dto.Description);
        a.Slug = curentTime.ToString("yyyyMMddHHmmmmss") + "-" + san.Sanitize(dto.Slug).Replace(" ", "-");

        if (dto.ImageFile != null)
        {
            if (dto.ImageFile.Length > 10100000)
            {
                return ArtistDtoResult.ImageLarge;
            }

            var newName = a.Name + "-" + curentTime.ToString("ssmmMMddHHyyyy");

            var imageName = TextFixer.FixTextForUrl(newName) + Path.GetExtension(dto.ImageFile.FileName);

            var res = dto.ImageFile.AddImageToServer(imageName, PathExtension.ProductImageServer,
                300, 300, PathExtension.ProductImageThumbServer, a.ImageName);

            if (!res)
            {
                return ArtistDtoResult.ImageNotUploaded;
            }
            a.ImageName = imageName;
        }
        else
        {
            if (a.ImageName == null)
                a.ImageName = "1.png";
        }

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