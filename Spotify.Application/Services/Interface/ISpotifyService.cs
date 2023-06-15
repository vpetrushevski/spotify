using Spotify.DTO.Request;
using Spotify.DTO.Response;

namespace Spotify.Application.Services.Interface
{
    public interface ISpotifyService
    {
        Task<List<ArtistResponse>> GetArtists(ArtistPagedRequest request);
        Task<ArtistResponse> GetArtistDetails(string id);
    }
}
