namespace Art.Gallery.Emails;
public class MailSender : IMailSender
{

    #region Constructor

    private readonly IViewRenderService _viewRenderService;

    public MailSender(IViewRenderService viewRenderService)
    {
        _viewRenderService = viewRenderService;
    }

    #endregion

    #region Mail

    public MailResult SendEmail(MailDTO dto)
    {
        try
        {
            string body = _viewRenderService.RenderToString("Emails/_MailView", dto);
            var res = EmailService.SendEmail(dto.Email, dto.Title, body);
            if (!res)
            {
                EmailService.SendEmail(dto.Email, dto.Title, body);
                return MailResult.Success;
            }
            return MailResult.Success;
        }
        catch (Exception)
        {
            return MailResult.Error;
        }
    }

    #endregion

}