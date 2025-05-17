namespace Art.Gallery.Data.Dtos.Paging;
public class Pager
{
    public static BasePaging Build(int pageId, int allEntitiesCount, int take, int howManyShowPageAfterAndBefore)
    {
        if (pageId == 0)
            pageId = 1;
        if (howManyShowPageAfterAndBefore == 0)
            howManyShowPageAfterAndBefore = 3;
        if (take == 0)
            take = 15;

        var pageCount = Convert.ToInt32(Math.Ceiling(allEntitiesCount / (double)take));
        return new BasePaging
        {
            PageId = pageId,
            AllEntitiesCount = allEntitiesCount,
            TakeEntity = take,
            SkipEntity = (pageId - 1) * take,
            StartPage = pageId - howManyShowPageAfterAndBefore <= 0 ? 1 : pageId - howManyShowPageAfterAndBefore,
            EndPage = pageId + howManyShowPageAfterAndBefore > pageCount ? pageCount : pageId + howManyShowPageAfterAndBefore,
            HowManyShowPageAfterAndBefore = howManyShowPageAfterAndBefore,
            PageCount = pageCount
        };
    }
}