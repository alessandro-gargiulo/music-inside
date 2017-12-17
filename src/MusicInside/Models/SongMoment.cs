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
