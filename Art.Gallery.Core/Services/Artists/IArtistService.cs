using Art.Gallery.Data.Dtos.Artists;
namespace Art.Gallery.Core.Services.Artists;
public interface IArtistService
{

    #region Artist

    ArtistDtoResult AddArtist(CEArtistDto dto);

    ArtistDtoResult DeleteArtist(string id, string userId);

    ArtistDtoResult RecoverArtist(string id, string userId);

    ArtistDtoResult ActiveArtist(string id);

    ArtistDtoResult RejectArtist(string id);

    ArtistDtoResult UpdateArtist(CEArtistDto dto);

    CEArtistDto GetArtist(string id, string userId);

    CEArtistDto GetArtistForShow(string id, string userName);

    Task<FilterArtistDto> FilterArtist(FilterArtistDto dto);

    #endregion

}