using Art.Gallery.Common;
using Art.Gallery.Core.Services.Account;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Paging;
using Art.Gallery.Data.Dtos.Products;
using Art.Gallery.Data.Entities.Products;
using Ganss.Xss;
using Microsoft.EntityFrameworkCore;
namespace Art.Gallery.Core.Services.Products;
public class ProductsService : IProductsService
{

    #region Products

    private readonly SiteDBContext _db;
    private readonly UrlProtector _urlProtector;
    readonly IAccountService _accountService;

    public ProductsService(SiteDBContext db, UrlProtector urlProtector, IAccountService accountService)
    {
        _db = db;
        _urlProtector = urlProtector;
        _accountService = accountService;
    }

    // Get Last Products
    public List<ProductDto> GetLastProducts()
    {
        var p = _db.Products
            .Where(a => !a.IsDelete && a.IsActive)
            .OrderByDescending(p => p.CreateDate)
            .Skip(0)
            .Take(6)
            .Distinct()
            .ToList();

        if (p.Count == 0)
            return new List<ProductDto>();

        List<ProductDto> dtos = new List<ProductDto>();

        foreach (var product in p)
        {
            var a = new ProductDto()
            {
                Name = product.Name,
                Slug = product.Slug,
                Price = (decimal)product.Price,
                ImageName = PathExtension.DomainAddress +
                PathExtension.ProductImage +
                product.ImageName,
            };
            dtos.Add(a);
        }

        return dtos;
    }

    // Get Special Products
    public List<ProductDto> GetSpecialProducts()
    {
        var p = _db.Products
            .Where(a => !a.IsDelete && a.IsSpecial && a.IsActive)
            .OrderByDescending(p => p.CreateDate)
            .Skip(0)
            .Take(3)
            .Distinct()
            .ToList();

        if (p.Count == 0)
            return new List<ProductDto>();

        List<ProductDto> dtos = new List<ProductDto>();

        foreach (var product in p)
        {
            var a = new ProductDto()
            {
                Name = product.Name,
                Slug = product.Slug,
                Price = (decimal)product.Price,
                ImageName = PathExtension.DomainAddress +
                PathExtension.ProductImage + product.ImageName,
                IsSpecial = product.IsSpecial,
            };
            dtos.Add(a);
        }

        return dtos;
    }

    // Add
    public ProductResult AddProduct(CEProductDto dto)
    {
        try
        {
            if (dto.Name == null)
                return ProductResult.Null;

            long userId = Convert.ToInt64(_urlProtector.UnProtect(dto.UserId));
            long artistId = Convert.ToInt64(_urlProtector.UnProtect(dto.ArtistId));

            Product p = new Product();

            DateTime curentTime = DateTime.Now;

            var newName = dto.Slug + "-" + curentTime.ToString("yyyyMMddHHmmss");

            if (dto.ImageFile != null)
            {
                if (dto.ImageFile.Length > 10100000)
                {
                    return ProductResult.ImageLarge;
                }

                var imageName = TextFixer.FixTextForUrl(newName) + Path.GetExtension(dto.ImageFile.FileName);

                var res = dto.ImageFile.AddImageToServer(imageName, PathExtension.ProductImageServer,
                    300, 300, PathExtension.ProductImageThumbServer, null);

                if (!res)
                {
                    return ProductResult.ImageNotUploaded;
                }
                p.ImageName = imageName;
            }
            else
            {
                p.ImageName = "1.png";
            }

            HtmlSanitizer san = new HtmlSanitizer();

            p.Name = san.Sanitize(dto.Name);
            p.Description = san.Sanitize(dto.Description);
            // slug with date
            p.Slug = san.Sanitize(newName);
            p.IsSpecial = dto.IsSpecial;
            p.Count = dto.Count;
            p.Price = dto.Price;
            p.ArtistId = artistId;
            p.UserId = userId;

            p.CreateDate = curentTime;
            p.UpdateDate = curentTime;

            _db.Products.Add(p);
            _db.SaveChanges();

            return ProductResult.Success;
        }
        catch (Exception)
        {
            return ProductResult.Error;
        }
    }
    // Delete
    public ProductResult DeleteProduct(string productId, string userId)
    {
        try
        {
            // product
            long pId = Convert.ToInt64(_urlProtector.UnProtect(productId));
            // user
            long uId = Convert.ToInt64(_urlProtector.UnProtect(userId));

            // query
            var p = _db.Products.FirstOrDefault(a => a.Id == pId && a.UserId == uId);

            if (p == null)
                return ProductResult.Null;

            // update

            p.IsDelete = true;

            _db.Products.Update(p);
            _db.SaveChanges();

            return ProductResult.Success;
        }
        catch (Exception)
        {
            return ProductResult.Error;
        }
    }

    // Filter
    public async Task<FilterProductsDto> FilterProductsAsync(FilterProductsDto dto)
    {
        var query = _db
            .Products
            .Include(c => c.Artist)
            .AsQueryable();

        if (!string.IsNullOrEmpty(dto.Name))
            query = query.Where(s => EF.Functions.Like(s.Name, $"%{dto.Name}%"));

        if (dto.CategoryId != 0)
            query = query.Where(s => s.ProductSelectedCategories.Any(f => f.Category.Id == dto.CategoryId));

        if (!string.IsNullOrEmpty(dto.ArtistId))
            query = query.Where(s => s.ArtistId == Convert.ToInt64(_urlProtector.UnProtect(dto.ArtistId)));

        if (dto.UserId != null)
            if (!_accountService.IsAdmin(dto.UserId))
            {
                long id = Convert.ToInt64(_urlProtector.UnProtect(dto.UserId));
                query = query.Where(s => s.UserId == id);
            }

        if (dto.MinPrice != 0)
            query = query.Where(p => p.Price >= dto.MinPrice.Value);

        if (dto.MaxPrice != 0)
            query = query.Where(p => p.Price <= dto.MaxPrice.Value);

        // مرتب‌سازی
        query = dto.SortBy switch
        {
            "all" => query,
            "active" => query.Where(a => a.IsActive),
            "notActive" => query.Where(a => !a.IsActive),
            "newest" => query.OrderByDescending(p => p.CreateDate).Where(a => a.IsActive && !a.IsDelete),
            "cheapest" => query.OrderBy(p => p.Price).Where(a => a.IsActive && !a.IsDelete),
            "expensive" => query.OrderByDescending(p => p.Price).Where(a => a.IsActive && !a.IsDelete),
            _ => query
        };

        #region paging

        var pager = Pager.Build(dto.PageId, await query.CountAsync(), dto.TakeEntity, dto.HowManyShowPageAfterAndBefore);

        #endregion

        List<ProductDto> dtos = new List<ProductDto>();

        foreach (var item in query)
        {
            var a = new ProductDto
            {
                Id = _urlProtector.Protect(item.Id.ToString()),
                Name = item.Name,
                Slug = item.Slug,
                ImageName = PathExtension.DomainAddress +
                PathExtension.ProductImage + item.ImageName,
                Price = (decimal)item.Price,
                IsDelete = item.IsDelete,
                IsSpecial = item.IsSpecial,
                ArtistSlug = item.Artist.Slug,
                IsActive = item.IsActive,
                ArtistId = _urlProtector.Protect(item.ArtistId.ToString()),
            };
            dtos.Add(a);
        }

        return dto.SetProducts(dtos).SetPaging(pager);
    }

    // Get for Update
    public CEProductDto GetForUpdateProduct(string id)
    {
        long productId = Convert.ToInt64(_urlProtector.UnProtect(id));

        Product p = _db.Products.FirstOrDefault(a => a.Id == productId);

        if (p == null)
            return new CEProductDto();

        CEProductDto dto = new CEProductDto();

        dto.ProductId = _urlProtector.Protect(p.Id.ToString());
        dto.Name = p.Name;
        dto.Slug = p.Slug;
        dto.ImageName = PathExtension.DomainAddress +
                PathExtension.ProductImage + p.ImageName;
        dto.Price = p.Price;
        dto.Description = p.Description;
        dto.IsSpecial = p.IsSpecial;
        dto.Count = p.Count;

        return dto;
    }

    // Update
    public ProductResult UpdateProduct(CEProductDto dto)
    {
        try
        {
            if (dto.Name == null)
                return ProductResult.Null;

            long productId = Convert.ToInt64(_urlProtector.UnProtect(dto.ProductId));

            Product p = _db.Products.FirstOrDefault(p => p.Id == productId);

            if (p == null)
                return ProductResult.Null;

            DateTime curentTime = DateTime.Now;

            var newName = dto.Slug + "-" + curentTime.ToString("yyyyMMddHHmmss");

            if (dto.ImageFile != null)
            {
                if (dto.ImageFile.Length > 10100000)
                {
                    return ProductResult.ImageLarge;
                }

                var imageName = TextFixer.FixTextForUrl(newName) + Path.GetExtension(dto.ImageFile.FileName);

                var res = dto.ImageFile.AddImageToServer(imageName, PathExtension.ProductImageServer,
                    300, 300, PathExtension.ProductImageThumbServer, null);

                if (!res)
                {
                    return ProductResult.ImageNotUploaded;
                }
                p.ImageName = imageName;
            }
            HtmlSanitizer san = new HtmlSanitizer();

            p.Name = san.Sanitize(dto.Name);
            p.Count = dto.Count;
            p.Price = dto.Price;
            p.Slug = san.Sanitize(newName);
            p.Description = san.Sanitize(dto.Description);
            p.UpdateDate = DateTime.Now;
            p.IsActive = false;
            p.IsSpecial = dto.IsSpecial;

            _db.Products.Update(p);
            _db.SaveChanges();

            return ProductResult.Success;
        }
        catch (Exception)
        {
            return ProductResult.Error;
        }
    }

    // Recover Product
    public ProductResult RecoverProduct(string id, string artistId)
    {
        long productId = Convert.ToInt64(_urlProtector.UnProtect(id));
        long arId = Convert.ToInt64(_urlProtector.UnProtect(artistId));
        var a = _db.Products.FirstOrDefault(a => a.Id == productId && a.ArtistId == arId);

        if (a != null)
        {
            a.IsDelete = false;

            _db.Products.Update(a);
            _db.SaveChanges();
            return ProductResult.Success;
        }
        return ProductResult.Error;
    }

    // Active Product
    public ProductResult ActiveProduct(string id, string artistId)
    {
        long productId = Convert.ToInt64(_urlProtector.UnProtect(id));
        long arId = Convert.ToInt64(_urlProtector.UnProtect(artistId));
        var a = _db.Products.FirstOrDefault(a => a.Id == productId && a.ArtistId == arId);

        if (a != null)
        {
            a.IsActive = true;

            _db.Products.Update(a);
            _db.SaveChanges();
            return ProductResult.Success;
        }
        return ProductResult.Error;
    }

    // Reject Product
    public ProductResult RejectProduct(string id, string artistId)
    {
        long productId = Convert.ToInt64(_urlProtector.UnProtect(id));
        long arId = Convert.ToInt64(_urlProtector.UnProtect(artistId));
        var a = _db.Products.FirstOrDefault(a => a.Id == productId && a.ArtistId == arId);

        if (a != null)
        {
            a.IsActive = false;

            _db.Products.Update(a);
            _db.SaveChanges();
            return ProductResult.Success;
        }
        return ProductResult.Error;
    }

    // Get
    public async Task<ProductDto> GetProduct(string Slug)
    {
        if (string.IsNullOrEmpty(Slug))
            return null;

        var product = await _db.Products
            .Include(a => a.Artist)
            .FirstOrDefaultAsync(a =>
                !a.IsDelete && a.Slug == Slug);

        if (product == null)
            return null;

        ProductDto dto = new ProductDto();

        dto.Slug = product.Slug;
        dto.Name = product.Name;
        dto.ImageName = PathExtension.DomainAddress +
                PathExtension.ProductImage + product.ImageName;
        dto.Price = (decimal)product.Price;
        dto.Description = product.Description;
        dto.Count = product.Count;
        dto.IsSpecial = product.IsSpecial;
        dto.ArtistSlug = product.Artist.Slug;
        dto.Id = _urlProtector.Protect(product.Id.ToString());

        return dto;
    }

    #endregion

    public async ValueTask DisposeAsync()
    {
        if (_db != null)
            await _db.DisposeAsync();
    }

}