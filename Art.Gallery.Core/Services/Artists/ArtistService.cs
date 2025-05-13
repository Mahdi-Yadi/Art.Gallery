using Art.Gallery.Common;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Artists;
using Art.Gallery.Data.Dtos.Paging;
using Art.Gallery.Data.Entities.Account;
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

        a.Name = san.Sanitize(dto.Name);
        a.Description = san.Sanitize(dto.Description);
        a.Slug = san.Sanitize(dto.Slug);
        a.UserId = Convert.ToInt64(_urlProtector.UnProtect(dto.UserId));

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
    public ArtistDtoResult DeleteArtist(string id, string userId)
    {
        var a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(id) &&
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

    public ArtistDtoResult ActiveArtist(string id)
    {
        var a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(id));

        if (a != null)
        {
            a.IsActive = true;

            _db.Artists.Update(a);
            _db.SaveChanges();
            return ArtistDtoResult.Success;
        }
        return ArtistDtoResult.Error;
    }

    public ArtistDtoResult RejectArtist(string id)
    {
        var a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(id));

        if (a != null)
        {
            a.IsActive = false;

            _db.Artists.Update(a);
            _db.SaveChanges();
            return ArtistDtoResult.Success;
        }
        return ArtistDtoResult.Error;
    }

    // Recover
    public ArtistDtoResult RecoverArtist(string id, string userId)
    {
        var a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(id) &&
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

    public async Task<FilterArtistDto> FilterArtist(FilterArtistDto dto)
    {
        var query = _db
            .Artists
            .Where(a => !a.IsDelete)
            .Include(a => a.Products)
            .AsQueryable();

        if (!string.IsNullOrEmpty(dto.Name))
            query = query.Where(s => EF.Functions.Like(s.Name, $"%{dto.Name}%"));

        if (dto.UserId != null)
        {
            long userId = Convert.ToInt64(_urlProtector.UnProtect(dto.UserId));
            if (userId != 0)
                query = query.Where(s => s.UserId == userId);
        }

        query = dto.SortBy switch
        {
            "newest" => query.OrderByDescending(p => p.CreateDate),
            "oldest" => query.OrderBy(p => p.CreateDate),
            _ => query.OrderByDescending(p => p.Id)
        };

        var totalCount = await query.CountAsync();
        var pager = Pager.Build(dto.PageId, totalCount, dto.TakeEntity, dto.HowManyShowPageAfterAndBefore);

        var artists = await query
            .Skip(pager.SkipEntity)
            .Take(pager.TakeEntity)
            .Select(p => new CEArtistDto
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                Slug = p.Slug,
                ImageName = p.ImageName
            })
            .ToListAsync();

        var result = dto.SetArtists(artists).SetPaging(pager);

        return result;
    }

    public CEArtistDto GetArtistForShow(string id, string userName)
    {
        Artist a = _db.Artists.FirstOrDefault(a =>
        !a.IsActive && a.Id == Convert.ToInt64(id) &&
        a.Name == userName);

        if (a == null)
            return null;

        CEArtistDto dto = new CEArtistDto();

        dto.Name = a.Name;
        dto.Description = a.Description;
        dto.Slug = a.Slug;
        dto.ImageName = a.ImageName;

        return dto;
    }

    public CEArtistDto GetArtist(string id, string userId)
    {
        Artist a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(id) &&
        a.UserId == Convert.ToInt64(_urlProtector.UnProtect(userId)));

        if (a == null)
            return null;

        CEArtistDto dto = new CEArtistDto();

        dto.Name = a.Name;
        dto.Description = a.Description;
        dto.Slug = a.Slug;
        dto.ImageName = a.ImageName;

        return dto;
    }

    // Update 
    public ArtistDtoResult UpdateArtist(CEArtistDto dto)
    {
        HtmlSanitizer san = new HtmlSanitizer();

        Artist a = _db.Artists.FirstOrDefault(a => a.Id == Convert.ToInt64(dto.Id));

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