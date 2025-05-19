using Art.Gallery.Data.Dtos.Paging;
namespace Art.Gallery.Data.Dtos.Products;
public class FilterProductsDto : BasePaging
{
    public List<ProductDto> Products { get; set; }

    public string Name { get; set; }
    public string ArtistId { get; set; }
    public bool IsActive { get; set; }
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
        this.PageId = paging.PageId;
        this.AllEntitiesCount = paging.AllEntitiesCount;
        this.StartPage = paging.StartPage;
        this.EndPage = paging.EndPage;
        this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
        this.TakeEntity = paging.TakeEntity;
        this.SkipEntity = paging.SkipEntity;
        this.PageCount = paging.PageCount;
        return this;
    }
}