using System.Collections.Generic;

namespace MusicInside.Models
{
    public class Song
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int TrackNo { get; set; }
        public int Year { get; set; }
        public int? AlbumId { get; set; }
        public Album Album { get; set; }
        public File File { get; set; }
        public Statistic Statistic { get; set; }
        public List<SongMoment> SongMoments { get; set; }
        public List<SongGenre> SongGenres { get; set; }
        public List<Featuring> Featurings { get; set; }
    }
}
