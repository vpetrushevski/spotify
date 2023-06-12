using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.DTO.Response
{
    public class ArtistResponse
    {
        public ArtistExternalUrl external_urls { get; set; }
        public ArtistFollower followers { get; set; }
        public List<string> genres { get; set; }
        public string href { get; set; } = null!;
        public string id { get; set; } = null!;
        public List<ArtistImage> images { get; set; }
        public string name { get; set; } = null!;
        public int popularity { get; set; }
        public string type { get; set; } = null!;
        public string uri { get; set; } = null!;
    }

    public class ArtistExternalUrl
    {
        public string spotify { get; set; } = null!;
    }

    public class ArtistFollower
    {
        public string? href { get; set; }
        public int total { get; set; }
    }

    public class ArtistImage
    {
        public int height { get; set; }
        public string url { get; set; } = null!;
        public int width { get; set; }
    }
}
