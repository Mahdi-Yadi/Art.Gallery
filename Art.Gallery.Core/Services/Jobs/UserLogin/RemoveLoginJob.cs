using Art.Gallery.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Quartz;
namespace Art.Gallery.Core.Services.Jobs.UserLogin;
public class RemoveLoginJob : IJob
{
    private readonly SiteDBContext _db;

    public RemoveLoginJob(SiteDBContext db)
    {
        _db = db;
    }
    public async Task Execute(IJobExecutionContext context)
    {
        var tokens = await _db.RefreshTokens.Where(a => a.Expires < DateTime.Now).ToListAsync();

        if (tokens.Count > 0)
        {
            foreach (var item in tokens)
            {
                _db.Remove(item);
            }
            await _db.SaveChangesAsync();
        }
    }
}