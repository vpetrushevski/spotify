using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.DTO.Request
{
    public class ArtistPagedRequest
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
