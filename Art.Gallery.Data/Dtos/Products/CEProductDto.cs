using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace Art.Gallery.Data.Dtos.Products;
public class CEProductDto
{
    public string ProductId { get; set; }

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

    [Display(Name = "قیمت")]
    public decimal? Price { get; set; }

    [Display(Name = "موجودی")]
    public long? Count { get; set; }

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

    [Display(Name = "وضعیت ویژه")]
    public bool IsSpecial { get; set; }

    public List<long> CategoriesId { get; set; }

    public string UserId { get; set; }

    public string ArtistId { get; set; }

    public IFormFile ImageFile { get; set; }
}