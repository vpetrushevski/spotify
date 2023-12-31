﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spotify.API.Extensions;
using Spotify.Application.Services.Interface;
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
        [HttpGet("artist/details")]
        [ProducesResponseType(typeof(ApiResponse<ArtistResponse>), 200)]
        public async Task<IActionResult> GetArtists()
        {
            try
            {
                return this.SuccessResponse(await _spotifyService.GetArtists());
            }
            catch (Exception ex)
            {
                return this.FailResponse(ex);
            }
        }
    }
}
