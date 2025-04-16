namespace Art.Gallery.Emails;
public class MailDTO
{
    public string Title { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string ActiveCode { get; set; }
    public string Description { get; set; }
    public string ButtonTitle { get; set; }
    public string Link { get; set; }
    public DateTime CreateDate { get; set; }
}
public enum MailResult
{
    Success,
    Error
}