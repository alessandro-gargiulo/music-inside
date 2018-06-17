using MusicInside.Models.Models;

namespace MusicInside.Managers.Entities
{
    public class ESong
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int TrackNo { get; set; }
        public int Year { get; set; }

        public void CopyFromModel(Song song)
        {
            this.Id = song.Id;
            this.Title = song.Title;
            this.TrackNo = song.TrackNo;
            this.Year = song.Year;
        }
    }
}
