using Newtonsoft.Json;
using Spotify.Application.Services.Interface;
using Spotify.DTO.Configuration;
using Spotify.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly SpotifyConfig _spotifyConfig;

        public AuthService(SpotifyConfig spotifyConfig)
        {
            _spotifyConfig = spotifyConfig;
        }

        public async Task<string> GetSpotifyAccessToken()
        {
            HttpClient client = new HttpClient();

            var requestBody = new Dictionary<string, string>();
            requestBody.Add("grant_type", "client_credentials");

            var encodedCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", _spotifyConfig.ClientId, _spotifyConfig.ClientSecret)));

            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_spotifyConfig.ApiTokenUrl),
                Content = new FormUrlEncodedContent(requestBody)
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded")
                    }
                },
                Headers =
                {
                    { "Authorization", $"Basic {encodedCredentials}" }
                }
            };

            var response = await client.SendAsync(request);
            SpotifyTokenResponse responseObject = JsonConvert.DeserializeObject<SpotifyTokenResponse>(await response.Content.ReadAsStringAsync());

            return responseObject.access_token;
        }
    }
}
