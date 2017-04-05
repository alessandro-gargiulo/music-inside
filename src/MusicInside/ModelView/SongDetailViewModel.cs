using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.ModelView
{
    public class SongDetailViewModel
    {
        public int SongId { get; set; }
        public string ArtistLabel { get; set; }
        public string TitleLabel { get; set; }
        public string AlbumLabel { get; set; }
        public int Year { get; set; }
        public int TrackNo { get; set; }
        public string GenreLabel { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LastPlay { get; set; }
        public int NumOfPlays { get; set; }
        public int AlbumCoverFileId { get; set; }
    }
}
