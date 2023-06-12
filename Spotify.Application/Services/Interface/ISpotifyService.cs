using Spotify.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Application.Services.Interface
{
    public interface ISpotifyService
    {
        Task<ArtistResponse> GetArtists();
    }
}
