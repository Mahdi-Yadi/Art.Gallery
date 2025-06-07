using System.ComponentModel.DataAnnotations;
using Art.Gallery.Data.Entities.Common;
namespace Art.Gallery.Data.Entities.Site;
public class Slider : BaseEntity
{

    [MaxLength(300)]
    [DataType(DataType.Text)]
    public string Title { get; set; }

    [MaxLength(100)]
    [DataType(DataType.Text)]
    public string ImageName { get; set; }

    [MaxLength(500)]
    [DataType(DataType.Text)]
    public string Text { get; set; }

    [MaxLength(400)]
    [Url]
    public string Link { get; set; }

}
public enum SliderResult
{
    Success,
    Null,
    Exist,
    Error
}
