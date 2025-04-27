namespace Art.Gallery.Common;
public class PathExtension
{

    public static string DomainAddress = "https://localhost:44350";

    #region Product

    public static string ProductImage = "/CMS/Images/ProductImage/";

    public static string ProductImageThumb = "/CMS/Images/ProductImageThumb/";

    public static string ProductImageServer =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CMS/Images/ProductImage/");

    public static string ProductImageThumbServer =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CMS/Images/ProductImageThumb/");

    #endregion


}