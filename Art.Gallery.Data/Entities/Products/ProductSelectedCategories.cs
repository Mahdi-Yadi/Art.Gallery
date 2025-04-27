using Art.Gallery.Data.Entities.Categories;
using Art.Gallery.Data.Entities.Common;
namespace Art.Gallery.Data.Entities.Products;
public class ProductSelectedCategories : BaseEntity
{
    public long CategoryId { get; set; }    
    public long ProductId { get; set; }

    public Category Category { get; set; }

    public Product Product { get; set; }
}