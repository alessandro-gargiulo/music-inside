using System;

namespace MusicInside.WebApi.Contracts
{
    public class CSongDetail
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public int Year { get; set; }
        public int TrackNo { get; set; }
        public string Genres { get; set; }
        public DateTime LastPlay { get; set; }
        public int NumOfPlays { get; set; }
        public int AlbumCoverId { get; set; }
        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
    }
}
