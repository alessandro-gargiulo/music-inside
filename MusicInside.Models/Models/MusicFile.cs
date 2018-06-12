namespace MusicInside.Models.Models
{
    public class MusicFile
    {
        #region Properties
        public int Id { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        #endregion
    }

    public class Cover : MusicFile
    {
        #region Navigation Properties
        public Album Album { get; set; }
        public int? AlbumId { get; set; }
        #endregion
    }

    public class Media : MusicFile
    {
        #region Navigation Properties
        public Song Song { get; set; }
        public int? SongId { get; set; }
        #endregion
    }
}
