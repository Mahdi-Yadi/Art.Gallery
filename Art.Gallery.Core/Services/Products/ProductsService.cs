using Art.Gallery.Common;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Paging;
using Art.Gallery.Data.Dtos.Products;
using Art.Gallery.Data.Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
namespace Art.Gallery.Core.Services.Products;
public class ProductsService : IProductsService
{

    #region Products

    private readonly SiteDBContext _db;
    private readonly IMemoryCache _cache;

    public ProductsService(SiteDBContext db,
        IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
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

    public ProductResult AddProduct(Product product, IFormFile imageFile)
    {
        try
        {
            if (product.Name == null)
                return ProductResult.Null;

            Product p = new Product();

            if (imageFile != null)
            {
                if (imageFile.Length > 10100000)
                {
                    return ProductResult.ImageLarge;
                }

                DateTime curentTime = DateTime.Now;

                var newName = product.Slug + "-" + curentTime.ToString("yyyyMMddHHmmss");

                var imageName = TextFixer.FixTextForUrl(newName) + Path.GetExtension(imageFile.FileName);

                var res = imageFile.AddImageToServer(imageName, PathExtension.ProductImageServer,
                    300, 300, PathExtension.ProductImageThumbServer, null);

                if (!res)
                {
                    return ProductResult.ImageNotUploaded;
                }
                product.ImageName = imageName;
            }

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
        // ساختن کلید کش بر اساس پارامترهای ورودی
        var cacheKey = $"ProductsFilter_{dto.Name}_{dto.CategoryId}_{dto.MinPrice}_{dto.MaxPrice}_{dto.SortBy}_{dto.PageId}";

        // تلاش برای گرفتن از کش
        if (_cache.TryGetValue(cacheKey, out FilterProductsDto cachedResult))
        {
            return cachedResult;
        }

        // اگر در کش نبود، کوئری دیتابیس را اجرا کن
        var query = _db
            .Products
            .Where(a => !a.IsDelete)
            .Include(a => a.ProductSelectedCategories)
            .AsQueryable();

        if (!string.IsNullOrEmpty(dto.Name))
            query = query.Where(s => EF.Functions.Like(s.Name, $"%{dto.Name}%"));

        if (dto.CategoryId.HasValue)
            query = query.Where(s => s.ProductSelectedCategories.Any(f => f.Category.Id == dto.CategoryId));

        if (dto.MinPrice.HasValue)
            query = query.Where(p => p.Price >= dto.MinPrice.Value);

        if (dto.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= dto.MaxPrice.Value);

        // مرتب‌سازی
        query = dto.SortBy switch
        {
            "newest" => query.OrderByDescending(p => p.CreateDate),
            "cheapest" => query.OrderBy(p => p.Price),
            "expensive" => query.OrderByDescending(p => p.Price),
            _ => query.OrderByDescending(p => p.Id)
        };

        var totalCount = await query.CountAsync();
        var pager = Pager.Build(dto.PageId, totalCount, dto.TakeEntity, dto.HowManyShowPageAfterAndBefore);

        var products = await query
            .Skip(pager.SkipEntity)
            .Take(pager.TakeEntity)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Slug = p.Slug,
                ImageName = p.ImageName,
                Price = (decimal)p.Price
            })
            .ToListAsync();

        var result = dto.SetProducts(products).SetPaging(pager);

        // ذخیره کردن نتیجه در کش
        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5)); // مثلا ۵ دقیقه اعتبار

        return result;
    }

    public Product GetForUpdateProduct(long id)
    {
        Product p = _db.Products.FirstOrDefault(p => p.Id == id);

        if (p == null)
            return new Product();

        return p;
    }

    public async Task<ProductDto> GetProduct(string Slug)
    {
        if (string.IsNullOrEmpty(Slug))
            return new ProductDto();

        var product = await _db.Products
                        .FirstOrDefaultAsync(a =>
                        !a.IsDelete && a.Slug == Slug);

        if (product == null)
            return new ProductDto();

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

    public ProductResult UpdateProduct(Product product, IFormFile imageFile)
    {
        try
        {
            if (product.Name == null)
                return ProductResult.Null;

            Product p = _db.Products.FirstOrDefault(p => p.Id == product.Id);

            if (p == null)
                return ProductResult.Null;

            if (imageFile != null)
            {
                if (imageFile.Length > 10100000)
                {
                    return ProductResult.ImageLarge;
                }

                DateTime curentTime = DateTime.Now;

                var newName = product.Slug + "-" + curentTime.ToString("yyyyMMddHHmmss");

                var imageName = TextFixer.FixTextForUrl(newName) + Path.GetExtension(imageFile.FileName);

                var res = imageFile.AddImageToServer(imageName, PathExtension.ProductImageServer,
                    300, 300, PathExtension.ProductImageThumbServer, null);

                if (!res)
                {
                    return ProductResult.ImageNotUploaded;
                }
                product.ImageName = imageName;
            }

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