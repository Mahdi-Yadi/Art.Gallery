using Art.Gallery.Data.Dtos.Artists;
namespace Art.Gallery.Core.Services.Artists;
public interface IArtistService
{

    #region Artist

    ArtistDtoResult AddArtist(CEArtistDto dto);

    ArtistDtoResult DeleteArtist(string artistId, string userId);

    ArtistDtoResult RecoverArtist(string artistId, string userId);

    ArtistDtoResult ActiveArtist(string artistId, string userId);

    ArtistDtoResult RejectArtist(string artistId, string userId);

    ArtistDtoResult UpdateArtist(CEArtistDto dto);

    CEArtistDto GetArtist(string artistId, string userId);

    CEArtistDto GetArtistForShow(string artistId, string userName);

    Task<FilterArtistDto> FilterArtist(FilterArtistDto dto);

    #endregion

}