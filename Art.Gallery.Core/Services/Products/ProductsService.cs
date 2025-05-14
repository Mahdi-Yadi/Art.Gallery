using Art.Gallery.Common;
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
    public ProductsService(SiteDBContext db,UrlProtector urlProtector)
    {
        _db = db;
        _urlProtector = urlProtector;
    }

    // Get Last Products
    public List<ProductDto> GetLastProducts()
    {
        var p = _db.Products
            .Where(a => !a.IsDelete)
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
                ImageName = product.ImageName,
            };
            dtos.Add(a);
        }

        return dtos;
    }

    // Get Special Products
    public List<ProductDto> GetSpecialProducts()
    {
        var p = _db.Products
            .Where(a => !a.IsDelete && a.IsSpecial)
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
                ImageName = product.ImageName,
                IsSpecial = product.IsSpecial,
            };
            dtos.Add(a);
        }

        return dtos;
    }

    public ProductResult AddProduct(CEProductDto dto)
    {
        try
        {
            if (dto.Name == null)
                return ProductResult.Null;

            Product p = new Product();

            if (dto.ImageFile != null)
            {
                if (dto.ImageFile.Length > 10100000)
                {
                    return ProductResult.ImageLarge;
                }

                DateTime curentTime = DateTime.Now;

                var newName = dto.Slug + "-" + curentTime.ToString("yyyyMMddHHmmss");

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
            p.Slug = san.Sanitize(dto.Slug);
            p.IsSpecial = dto.IsSpecial;
            p.Count = dto.Count;
            p.Price = dto.Price;
            p.ArtistId = dto.ArtistId;
            p.UserId = Convert.ToInt64(_urlProtector.UnProtect(dto.UserId));

            p.CreateDate = DateTime.Now;
            p.UpdateDate = DateTime.Now;

            _db.Products.Add(p);
            _db.SaveChanges();

            return ProductResult.Success;
        }
        catch (Exception)
        {
            return ProductResult.Error;
        }
    }

    public ProductResult DeleteProduct(long id)
    {
        Product p = _db.Products.FirstOrDefault(p => p.Id == id);

        if (p == null)
            return ProductResult.Null;

        p.IsDelete = true;

        _db.SaveChanges();
        return ProductResult.Success;
    }

    public async Task<FilterProductsDto> FilterProductsAsync(FilterProductsDto dto)
    {
        var query = _db.Products.AsQueryable();

       if(dto.TakeEntity == 0)
           dto.TakeEntity = 15;

        if (!string.IsNullOrEmpty(dto.Name))
            query = query.Where(s => EF.Functions.Like(s.Name, $"%{dto.Name}%"));

        if (dto.CategoryId != 0)
            query = query.Where(s => s.ProductSelectedCategories.Any(f => f.Category.Id == dto.CategoryId));

        if (dto.ArtistId != null)
            query = query.Where(s => s.ArtistId == Convert.ToInt64(dto.ArtistId));

        if (dto.MinPrice != 0)
            query = query.Where(p => p.Price >= dto.MinPrice.Value);

        if (dto.MaxPrice != 0)
            query = query.Where(p => p.Price <= dto.MaxPrice.Value);

        // مرتب‌سازی
        query = dto.SortBy switch
        {
            "newest" => query.OrderByDescending(p => p.CreateDate),
            "cheapest" => query.OrderBy(p => p.Price),
            "expensive" => query.OrderByDescending(p => p.Price),
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
                Id = item.Id,
                Name = item.Name,
                Slug = item.Slug,
                ImageName = item.ImageName,
                Price = (decimal)item.Price
            };
            dtos.Add(a);
        }

        return dto.SetProducts(dtos).SetPaging(pager);
    }

    public CEProductDto GetForUpdateProduct(long id)
    {
        Product p = _db.Products.FirstOrDefault(p => p.Id == id);

        if (p == null)
            return new CEProductDto();

        CEProductDto dto = new CEProductDto();

        dto.Name = p.Name;
        dto.Slug = p.Slug;
        dto.ImageName = p.ImageName;
        dto.Price = p.Price;
        dto.Description = p.Description;
        dto.IsSpecial = p.IsSpecial;
        dto.Count = p.Count;

        return dto;
    }

    public async Task<ProductDto> GetProduct(string Slug)
    {
        if (string.IsNullOrEmpty(Slug))
            return null;

        var product = await _db.Products
                        .FirstOrDefaultAsync(a =>
                        !a.IsDelete && a.Slug == Slug);

        if (product == null)
            return null;

        ProductDto dto = new ProductDto();

        dto.Slug = product.Slug;
        dto.Name = product.Name;
        dto.ImageName = product.ImageName;
        dto.Price = (decimal)product.Price;
        dto.Description = product.Description;
        dto.IsSpecial = product.IsSpecial;
        // encript
        dto.Id = product.Id;

        return dto;
    }

    public ProductResult UpdateProduct(CEProductDto dto)
    {
        try
        {
            if (dto.Name == null)
                return ProductResult.Null;

            Product p = _db.Products.FirstOrDefault(p => p.Id == Convert.ToInt64(dto.Id));

            if (p == null)
                return ProductResult.Null;

            if (dto.ImageFile != null)
            {
                if (dto.ImageFile.Length > 10100000)
                {
                    return ProductResult.ImageLarge;
                }

                DateTime curentTime = DateTime.Now;

                var newName = dto.Slug + "-" + curentTime.ToString("yyyyMMddHHmmss");

                var imageName = TextFixer.FixTextForUrl(newName) + Path.GetExtension(dto.ImageFile.FileName);

                var res = dto.ImageFile.AddImageToServer(imageName, PathExtension.ProductImageServer,
                    300, 300, PathExtension.ProductImageThumbServer, null);

                if (!res)
                {
                    return ProductResult.ImageNotUploaded;
                }
                dto.ImageName = imageName;
            }
            HtmlSanitizer san = new HtmlSanitizer();

            p.Name = san.Sanitize(dto.Name);
            p.Name = san.Sanitize(dto.Name);
            p.Name = san.Sanitize(dto.Name);
            p.ArtistId = dto.ArtistId;
            p.UpdateDate = DateTime.Now;

            _db.Products.Update(p);
            _db.SaveChanges();

            return ProductResult.Success;
        }
        catch (Exception)
        {
            return ProductResult.Error;
        }
    }

    #endregion

    public async ValueTask DisposeAsync()
    {
        if (_db != null)
            await _db.DisposeAsync();
    }

}