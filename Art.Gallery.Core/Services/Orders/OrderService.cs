using Art.Gallery.Data.Contexts;
namespace Art.Gallery.Core.Services.Orders;
public class OrderService : IOrderService
{

    private readonly SiteDBContext _db;

    public OrderService(SiteDBContext db)
    {
        _db = db;
    }



    public async ValueTask DisposeAsync()
    {
        if(_db != null) 
            await _db.DisposeAsync();
    }
}