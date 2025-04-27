using Art.Gallery.Data.Entities.Account;
using Art.Gallery.Data.Entities.Common;
using Art.Gallery.Data.Entities.Products;
using System.ComponentModel.DataAnnotations;
namespace Art.Gallery.Data.Entities.Artists;
public class Artist : BaseEntity
{

    [Display(Name = "نام محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Text)]
    [MaxLength(1000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(2, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Name { get; set; }

    [Display(Name = "متن")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Text)]
    [MaxLength(10000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(2, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Description { get; set; }

    [Display(Name = "متن آدرس محصول")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Text)]
    [MaxLength(1000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(2, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Slug { get; set; }

    [Display(Name = "نام تصویر")]
    [DataType(DataType.Text)]
    [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string ImageName { get; set; }

    public long UserId { get; set; }

    public User User { get; set; }

    public ICollection<Product> Products { get; set; }
}