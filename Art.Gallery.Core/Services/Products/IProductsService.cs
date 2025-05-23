using Art.Gallery.Data.Dtos.Artists;
using Art.Gallery.Data.Dtos.Products;
namespace Art.Gallery.Core.Services.Products;
public interface IProductsService : IAsyncDisposable
{

    #region Products

    // filter
    Task<FilterProductsDto> FilterProductsAsync(FilterProductsDto dto);
    // Get Project 
    Task<ProductDto> GetProduct(string Slug);
    // Add Product
    ProductResult AddProduct(CEProductDto dto);
    // Get Product For Update
    CEProductDto GetForUpdateProduct(string id);
    // Update Product
    ProductResult UpdateProduct(CEProductDto dto);
    // Delete Product
    ProductResult DeleteProduct(string id);
    // Get Last Product
    List<ProductDto> GetLastProducts();
    // Get Special Product
    List<ProductDto> GetSpecialProducts();
    // Recover Product
    ProductResult RecoverProduct(string id, string userId);
    // Active Product
    ProductResult ActiveProduct(string id, string userId);
    // Reject Product
    ProductResult RejectProduct(string id, string userId);

    #endregion

}