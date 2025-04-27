namespace Art.Gallery.Common;
public static class TextFixer
{
    public static string FixEmail(string email) => email.Trim().ToLower().Replace(" ", "");

    public static string FixTextForUrl(this string? text)
    {
        return text.Replace(" ", "-");
    }

    public static string FixTextForSearch(string text) => text.Trim().ToLower().Replace("-", " ");

    public static string FixNumber(string text) => text.Trim().Replace(",", "");

}