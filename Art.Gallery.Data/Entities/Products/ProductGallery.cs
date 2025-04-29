using Art.Gallery.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;
namespace Art.Gallery.Data.Entities.Products;
public class ProductGallery : BaseEntity
{

    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string ImageName { get; set; }

    public long ProductId { get; set; }

    public Product Product { get; set; }

}