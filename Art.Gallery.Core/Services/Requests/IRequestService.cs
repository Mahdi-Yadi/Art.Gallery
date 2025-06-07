namespace Art.Gallery.Core.Services.Requests;
public interface IRequestService : IAsyncDisposable
{

    #region Request

    Task<bool> CanUserSendRequestAsync(string userIp, string work);

    #endregion

}