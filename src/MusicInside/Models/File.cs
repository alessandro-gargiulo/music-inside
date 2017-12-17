namespace MusicInside.Models
{
    public class File
    {
        public int ID { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int? SongId { get; set; }
        public int? AlbumId { get; set; }
        public Song Song { get; set; }
        public Album Album { get; set; }
    }
}
