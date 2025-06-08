using Art.Gallery.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Art.Gallery.Data.Entities.Site;
public class SiteSetting : BaseEntity
{

    [Display(Name = "عنوان سایت")]
    [DataType(DataType.Text)]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(2, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Title { get; set; }

    [Display(Name = "ایمیل سایت")]
    [DataType(DataType.Text)]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(2, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Email { get; set; }

    [Display(Name = "آدرس سایت")]
    [DataType(DataType.Text)]
    [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(2, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Address { get; set; }

    [Display(Name = "توضیحات")]
    [DataType(DataType.Text)]
    [MaxLength(1000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(10, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Text { get; set; }

    [Display(Name = "عنوان شرکت")]
    [DataType(DataType.Text)]
    [MaxLength(1000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(2, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Name { get; set; }

    [Display(Name = "نام تصویر")]
    [DataType(DataType.Text)]
    [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string ImageName { get; set; }

    [Display(Name = "شماره تماس")]
    public string Phone { get; set; }  
}
public class SiteDto
{
    public string Id { get; set; }

    [Display(Name = "عنوان سایت")]
    [DataType(DataType.Text)]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(2, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Title { get; set; }

    [Display(Name = "ایمیل سایت")]
    [DataType(DataType.Text)]
    [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(2, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Email { get; set; }

    [Display(Name = "آدرس سایت")]
    [DataType(DataType.Text)]
    [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(2, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Address { get; set; }

    [Display(Name = "توضیحات")]
    [DataType(DataType.Text)]
    [MaxLength(1000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(10, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Text { get; set; }

    [Display(Name = "عنوان شرکت")]
    [DataType(DataType.Text)]
    [MaxLength(1000, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    [MinLength(2, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
    public string Name { get; set; }

    [Display(Name = "نام تصویر")]
    [DataType(DataType.Text)]
    [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string ImageName { get; set; }

    [Display(Name = "شماره تماس")]
    public string Phone { get; set; }

    public IFormFile ImageFile { get; set; }
}