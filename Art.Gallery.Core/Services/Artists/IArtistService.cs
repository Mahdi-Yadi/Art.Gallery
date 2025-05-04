using Art.Gallery.Data.Dtos.Artists;
namespace Art.Gallery.Core.Services.Artists;
public interface IArtistService
{

    #region Artist

    ArtistDtoResult AddArtist(CEArtistDto dto);

    ArtistDtoResult DeleteArtist(string id);

    ArtistDtoResult RecoverArtist(string id);

    ArtistDtoResult UpdateArtist(CEArtistDto dto);

    CEArtistDto GetArtist(string id);

    Task<FilterArtistDto> FilterArtist(FilterArtistDto dto);

    #endregion

}