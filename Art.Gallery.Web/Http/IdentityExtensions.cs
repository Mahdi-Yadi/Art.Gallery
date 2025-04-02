using System.Security.Claims;
using System.Security.Principal;
namespace Art.Gallery.Web.Http;
public static class IdentityExtensions
{
    public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal != null)
        {
            var data = claimsPrincipal.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
            if (data != null)
            {
                if (ulong.TryParse(data.Value, out var userId))
                {
                    return (long)userId;
                }
                else
                {
                    throw new OverflowException("Value was either too large or too small for a UInt64.");
                }
            }
        }

        return default(long);
    }

    public static long GetUserId(this IPrincipal principal)
    {
        var user = (ClaimsPrincipal)principal;
        return user.GetUserId();
    }
}