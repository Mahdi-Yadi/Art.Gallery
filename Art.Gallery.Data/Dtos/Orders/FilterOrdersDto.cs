using Art.Gallery.Data.Dtos.Paging;
namespace Art.Gallery.Data.Dtos.Orders;
public class FilterOrdersDto : BasePaging
{
    public List<OrderDto> OrderList { get; set; }

    public int OrderId { get; set; }

    public int Count { get; set; }

    public FilterOrdersDto SetOrders(List<OrderDto> orderDtos)
    {
        this.OrderList = orderDtos;
        return this;
    }

    public FilterOrdersDto SetPaging(BasePaging paging)
    {
        if (paging == null) throw new ArgumentNullException(nameof(paging));

        PageId = paging.PageId;
        AllEntitiesCount = paging.AllEntitiesCount;
        StartPage = paging.StartPage;
        EndPage = paging.EndPage;
        HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
        TakeEntity = paging.TakeEntity;
        SkipEntity = paging.SkipEntity;
        PageCount = paging.PageCount;

        return this;
    }

}