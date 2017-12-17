
using System.Collections.Generic;

namespace MusicInside.Models
{
    public class Genre
    {
        public Genre(){}
        public int ID { get; set; }
        public string Description { get; set; }
        public List<SongGenre> SongGenres { get; set; }
    }
}
