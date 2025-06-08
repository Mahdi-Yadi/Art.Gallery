using Art.Gallery.Data.Contexts;
using Art.Gallery.Data.Entities.Account;
using Art.Gallery.Emails;
using Microsoft.EntityFrameworkCore;
namespace Art.Gallery.Core.Services.Requests;
public class RequestService : IRequestService
{

    #region Constructor

    private readonly SiteDBContext _siteDBContext;
    private readonly IMailSender _mailSender;

    public RequestService(SiteDBContext siteDbContext, IMailSender mailSender)
    {
        _siteDBContext = siteDbContext;
        _mailSender = mailSender;
    }

    #endregion

    #region Request

    public async Task<bool> CanUserSendRequestAsync(string userIp, string work)
    {
        var thirtyMinutesAgo = DateTime.Now.AddMinutes(-30);

        var recentRequestsCount = await _siteDBContext
            .UserRequests
            .FirstOrDefaultAsync(c => c.UserIp == userIp &&
                                      c.CreateDate >= thirtyMinutesAgo &&
                                      c.Work.Contains(work));
        if (recentRequestsCount == null)
        {
            UserRequests requests = new UserRequests();
            requests.UserIp = userIp;
            requests.Work = work;
            requests.Count += 1;
            try
            {
                await _siteDBContext.AddAsync(requests);
                await _siteDBContext.SaveChangesAsync();
                recentRequestsCount = requests;
            }
            catch (Exception)
            {
                throw;
            }
        }
        else
        {
            try
            {
                recentRequestsCount.Count += 1;
                await _siteDBContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        return recentRequestsCount.Count > 4;
    }

    private void SendSecurityEmailToAdmin(string text)
    {
        try
        {
            var users = _siteDBContext.Users.Where(a => a.IsAdmin && !a.IsDelete && a.IsActive).ToList();

            MailDTO mailDTO = new MailDTO();

            if (users.Count > 0)
            {
                mailDTO.Title = "هشدار فعالیت مشکوک";
                mailDTO.Description = text;

                foreach (var item in users)
                {
                    mailDTO.Email = item.Email;

                    _mailSender.SendEmail(mailDTO);
                }
            }

        }
        catch (Exception)
        {
        }
    }

    #endregion

    #region Dispose

    public async ValueTask DisposeAsync()
    {
        if (_siteDBContext != null)
            await _siteDBContext.DisposeAsync();
    }

    #endregion

}