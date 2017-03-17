using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.Models
{
    public class SongMoment
    {
        public int SongId { get; set; }
        public int MomentId { get; set; }
        public Song Song { get; set; }
        public Moment Moment { get; set; }
    }
}
