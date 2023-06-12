using Newtonsoft.Json;
using Spotify.Application.Services.Interface;
using Spotify.DTO.Configuration;
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

        public async Task<ArtistResponse> GetArtists()
        {
            string accessToken = await _authService.GetSpotifyAccessToken();

            //string dataUrl = _spotifyConfig.ApiDataUrl + "artist";
            string dataUrl = $"{_spotifyConfig.ApiDataUrl}artists/0TnOYISbd1XYRBk9myaseg";

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
            return JsonConvert.DeserializeObject<ArtistResponse>(await response.Content.ReadAsStringAsync());
        }
    }
}
