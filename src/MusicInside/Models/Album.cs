using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.Models
{
    public class Album
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public bool IsSingle { get; set; }
        public int? FileId { get; set; }
        public int ArtistId { get; set; }
        public File File { get; set; }
        public Artist Artist { get; set; }
        public List<Song> Songs { get; set; }
    }
}
