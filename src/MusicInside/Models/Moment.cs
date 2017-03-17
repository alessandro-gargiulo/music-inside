using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.Models
{
    public class Moment
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public List<SongMoment> SongMoments { get; set; }
    }
}
