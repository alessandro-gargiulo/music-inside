using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.Models
{
    public class Featuring
    {
        public bool IsPrincipalArtist { get; set; }
        public int SongId { get; set; }
        public int ArtistId { get; set; }
        public Song Song { get; set; }
        public Artist Artist { get; set; }

    }
}
