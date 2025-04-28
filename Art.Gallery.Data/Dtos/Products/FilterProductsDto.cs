using Art.Gallery.Data.Dtos.Paging;
namespace Art.Gallery.Data.Dtos.Products;
public class FilterProductsDto : BasePaging
{
    public List<ProductDto> Products { get; set; }

    public string Name { get; set; }
    public long? CategoryId { get; set; }  // فیلتر براساس دسته‌بندی
    public decimal? MinPrice { get; set; } // فیلتر حداقل قیمت
    public decimal? MaxPrice { get; set; } // فیلتر حداکثر قیمت
    public string SortBy { get; set; }     // مرتب سازی: newest, cheapest, expensive

    public FilterProductsDto SetProducts(List<ProductDto> products)
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