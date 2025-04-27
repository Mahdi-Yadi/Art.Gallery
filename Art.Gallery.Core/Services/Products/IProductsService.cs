using Art.Gallery.Data.Dtos.Products;
using Art.Gallery.Data.Entities.Products;
using Microsoft.AspNetCore.Http;
namespace Art.Gallery.Core.Services.Products;
public interface IProductsService
{

    #region Products

    FilterProductsDto FilterProductsDto(FilterProductsDto dto);

    Product GetProduct(string Slug);

    ProductResult AddProduct(Product product, IFormFile imageFile);
    Product GetForUpdateProduct(long id);
    ProductResult UpdateProduct(Product product, IFormFile imageFile);
    ProductResult DeleteProduct(long id);

    #endregion

}