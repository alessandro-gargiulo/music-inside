using System;

namespace MusicInside.Models
{
    public class Statistic
    {
        public int ID { get; set; }
        public int NumPlay { get; set; }
        public DateTime LastPlay { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
