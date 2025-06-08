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
    ProductResult DeleteProduct(string productId,string userId);
    // Get Last Products
    List<ProductDto> GetLastProducts();
    // Get Special Products
    List<ProductDto> GetSpecialProducts();
    // Get Popular Products
    List<ProductDto> GetPopularProducts();
    // Get Best Selling Products
    List<ProductDto> GetBestSellingProducts();
    // Recover Product
    ProductResult RecoverProduct(string id, string artistId);
    // Active Product
    ProductResult ActiveProduct(string id, string artistId);
    // Reject Product
    ProductResult RejectProduct(string id, string artistId);

    #endregion

}