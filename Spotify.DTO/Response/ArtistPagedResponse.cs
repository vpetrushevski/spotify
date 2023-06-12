using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.DTO.Response
{
    public class ArtistPagedResponse
    {
        public Artists artists { get; set; }
    }

    public class Artists
    {
        public string href { get; set; } = null!;
        public List<ArtistResponse> items { get; set; }
        public int limit { get; set; }
        public string next { get; set; } = null!;
        public int offset { get; set; }
        public string? previous { get; set; }
        public int total { get; set; }
    }
}
