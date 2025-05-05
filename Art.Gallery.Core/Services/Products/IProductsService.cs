using Art.Gallery.Data.Dtos.Products;
using Art.Gallery.Data.Entities.Products;
using Microsoft.AspNetCore.Http;
namespace Art.Gallery.Core.Services.Products;
public interface IProductsService : IAsyncDisposable
{

    #region Products

    Task<FilterProductsDto> FilterProductsAsync(FilterProductsDto dto);

    Task<ProductDto> GetProduct(string Slug);

    ProductResult AddProduct(CEProductDto dto);
    Product GetForUpdateProduct(long id);
    ProductResult UpdateProduct(CEProductDto dto);
    ProductResult DeleteProduct(long id);

    List<ProductDto> GetLastProducts();

    List<ProductDto> GetSpecialProducts();

    #endregion

}