using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.Models
{
    public class Artist
    {
        public int ID { get; set; }
        public string ArtName { get; set; }
        public string Name { get; set; } // TO DELETE
        public string Surname { get; set; } // TO DELETE
        public DateTime BirthYear { get; set; }
        public List<Album> Albums { get; set; }
        public List<Featuring> Featurings { get; set; }
    }
}
