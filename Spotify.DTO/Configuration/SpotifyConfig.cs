using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.DTO.Configuration
{
    public class SpotifyConfig
    {
        public string ApiTokenUrl { get; set; } = null!;
        public string ApiDataUrl { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
    }
}
