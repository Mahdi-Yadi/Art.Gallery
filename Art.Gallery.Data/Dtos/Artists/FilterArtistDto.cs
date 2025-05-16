using Art.Gallery.Data.Dtos.Paging;
namespace Art.Gallery.Data.Dtos.Artists;
public class FilterArtistDto : BasePaging
{

    public List<ArtistDto> ArtistsDto { get; set; }

    public string Name { get; set; }

    public string SortBy { get; set; }

    public bool IsActive { get; set; }

    public string UserId { get; set; }

    public FilterArtistDto SetArtists(List<ArtistDto> artistDtos)
    {
        this.ArtistsDto = artistDtos;
        return this;
    }

    public FilterArtistDto SetPaging(BasePaging paging)
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