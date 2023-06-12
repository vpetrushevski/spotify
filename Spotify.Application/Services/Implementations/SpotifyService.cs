using Newtonsoft.Json;
using Spotify.Application.Services.Interface;
using Spotify.DTO.Configuration;
using Spotify.DTO.Request;
using Spotify.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.Services.Implementations
{
    public class SpotifyService : ISpotifyService
    {
        private readonly IAuthService _authService;
        private readonly SpotifyConfig _spotifyConfig;

        public SpotifyService(IAuthService authService, SpotifyConfig spotifyConfig)
        {
            _authService = authService;
            _spotifyConfig = spotifyConfig;
        }

        public async Task<List<ArtistResponse>> GetArtists(ArtistPagedRequest request)
        {
            string accessToken = await _authService.GetSpotifyAccessToken();

            int offset = (request.pageNumber * request.pageSize) - request.pageSize;
            string dataUrl = $"{_spotifyConfig.ApiDataUrl}search?query=ArtistName&type=artist&offset={offset}&limit={request.pageSize}";

            HttpClient client = new HttpClient();

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(dataUrl),
                Headers =
                {
                    {  "Authorization", $"Bearer {accessToken}" }
                }
            };

            var response = await client.SendAsync(requestMessage);
            ArtistPagedResponse artistsResponse = JsonConvert.DeserializeObject<ArtistPagedResponse>(await response.Content.ReadAsStringAsync());

            return artistsResponse.artists.items;
        }
    }
}
