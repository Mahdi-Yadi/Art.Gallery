using Art.Gallery.Common;
using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Dtos.Paging;
using Art.Gallery.Data.Dtos.Products;
using Art.Gallery.Data.Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Art.Gallery.Core.Services.Products;
public class ProductsService : IProductsService
{

    #region Products

    private readonly SiteDBContext _db;

    public ProductsService(SiteDBContext db)
    {
        _db = db;
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

    public FilterProductsDto FilterProductsDto(FilterProductsDto dto)
    {
        var query = _db.Products.AsQueryable().ToList();

        //if (!string.IsNullOrEmpty(dto.title))
        //    query = query.Where(s => EF.Functions.Like(s.Title, $"%{dto.title}%"));

        List<Product> dtos = new List<Product>();

        var totalCount = query.Count();
        var pager = Pager.Build(dto.PageId, totalCount,
            dto.TakeEntity, dto.HowManyShowPageAfterAndBefore);

        return dto.SetProducts(dtos).SetPaging(pager);
    }

    public Product GetForUpdateProduct(long id)
    {
        Product p = _db.Products.FirstOrDefault(p => p.Id == id);

        if (p == null)
            return new Product();

        return p;
    }

    public Product GetProduct(string Slug)
    {
        if (string.IsNullOrEmpty(Slug))
            return new Product();

        var product = _db.Products.FirstOrDefault(a => a.Slug == Slug);

        if (product == null)
            return new Product();

        return product; 
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

}