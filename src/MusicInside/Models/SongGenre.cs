using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.Models
{
    public class SongGenre
    {
        public int SongId { get; set; }
        public int GenreId { get; set; }
        public Song Song { get; set; }
        public Genre Genre { get; set; }
    }
}
