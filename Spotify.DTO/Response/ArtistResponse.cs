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
        public string href { get; set; }
        public string id { get; set; }
        public List<ArtistImage> images { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class ArtistExternalUrl
    {
        public string spotify { get; set; }
    }

    public class ArtistFollower
    {
        public string? href { get; set; }
        public int total { get; set; }
    }

    public class ArtistImage
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }
}
