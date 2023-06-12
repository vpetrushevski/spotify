using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.DTO.Response
{
    public class SpotifyTokenResponse
    {
        public string access_token { get; set; } = null!;
        public string token_type { get; set; } = null!;
        public int expires_in { get; set; }
    }
}
