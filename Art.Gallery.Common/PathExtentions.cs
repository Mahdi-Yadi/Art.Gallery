namespace Art.Gallery.Common;
public class PathExtension
{

    public static string DomainAddress = "https://localhost:44350";

    #region Site

    public static string SiteImage = "/CMS/Images/Site/";

    public static string SiteImageServer =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CMS/Images/Site/");

    #endregion

    #region Product

    public static string ProductImage = "/CMS/Images/ProductImage/";

    public static string ProductImageThumb = "/CMS/Images/ProductImageThumb/";

    public static string ProductImageServer =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CMS/Images/ProductImage/");

    public static string ProductImageThumbServer =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CMS/Images/ProductImageThumb/");

    #endregion

    #region Artist

    public static string ArtistImage = "/CMS/Images/ArtistImage/";

    public static string ArtistImageThumb = "/CMS/Images/ArtistImageThumb/";

    public static string ArtistImageServer =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CMS/Images/ArtistImage/");

    public static string ArtistImageThumbServer =
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CMS/Images/ArtistImageThumb/");

    #endregion

}