using System.Collections.Generic;

namespace MusicInside.Models
{
    public class Moment
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public List<SongMoment> SongMoments { get; set; }
    }
}
