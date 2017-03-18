using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.ModelView
{
    public class SongDetailViewModel
    {
        public string ArtistLabel { get; set; }
        public string TitleLabel { get; set; }
        public string AlbumLabel { get; set; }
        public int Year { get; set; }
        public int TrackNo { get; set; }
        public string GenreLabel { get; set; }
        public DateTime LastPlay { get; set; }
        public int NumOfPlays { get; set; }
        public int AlbumCoverFileId { get; set; } // DUBBI
    }
}
