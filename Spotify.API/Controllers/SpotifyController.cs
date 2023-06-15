using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spotify.API.Extensions;
using Spotify.Application.Services.Interface;
using Spotify.DTO.Request;
using Spotify.DTO.Response;
using System.ComponentModel.DataAnnotations;

namespace Spotify.API.Controllers
{
    [Route("spotify")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private readonly ISpotifyService _spotifyService;

        public SpotifyController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        /// <summary>
        /// Get artist details
        /// </summary>
        [HttpGet("artist/{id}")]
        [ProducesResponseType(typeof(ApiResponse<List<ArtistResponse>>), 200)]
        public async Task<IActionResult> GetArtistDetails(string id)
        {
            try
            {
                return this.SuccessResponse(await _spotifyService.GetArtistDetails(id));
            }
            catch (Exception ex)
            {
                return this.FailResponse(ex);
            }
        }

        /// <summary>
        /// Get artists
        /// </summary>
        [HttpPost("artist/paged")]
        [ProducesResponseType(typeof(ApiResponse<List<ArtistResponse>>), 200)]
        public async Task<IActionResult> GetArtists([FromBody] ArtistPagedRequest request)
        {
            try
            {
                return this.SuccessResponse(await _spotifyService.GetArtists(request));
            }
            catch (Exception ex)
            {
                return this.FailResponse(ex);
            }
        }
    }
}
