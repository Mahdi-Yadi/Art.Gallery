namespace Art.Gallery.Emails;
public interface IMailSender
{

    #region Account

    MailResult SendEmail(MailDTO dto);

    #endregion

}