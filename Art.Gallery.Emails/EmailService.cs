using System.Net.Mail;
using System.Net;
namespace Art.Gallery.Emails;
public class EmailService
{
    public static bool SendEmail(string EmailAddress, string Subject, string Body)
    {
        var result = false;
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("mail.technoto.org");
            mail.From = new MailAddress("gallery@technoto.org", "گالری Technoto");
            mail.To.Add(EmailAddress);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.EnableSsl = true;
            SmtpServer.Credentials = new NetworkCredential("gallery@technoto.org", "tfbdmas#4mcs3424rcxCLDLD");
            SmtpServer.Send(mail);
            result = true;
        }
        catch (Exception)
        {
            result = false;
        }
        return result;
    }
}