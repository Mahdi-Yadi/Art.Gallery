using Art.Gallery.Data.Dtos.Paging;
using Art.Gallery.Data.Entities.Products;
namespace Art.Gallery.Data.Dtos.Products;
public class FilterProductsDto : BasePaging
{
    public List<Product> Products { get; set; }

    public string title { get; set; }

    public FilterProductsDto SetProducts(List<Product> products)
    {
        this.Products = products;
        return this;
    }

    public FilterProductsDto SetPaging(BasePaging paging)
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