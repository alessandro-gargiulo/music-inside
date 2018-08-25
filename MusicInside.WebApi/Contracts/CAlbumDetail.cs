using System.Collections.Generic;

namespace MusicInside.WebApi.Contracts
{
    public class CAlbumDetail
    {
        public CAlbumDetail()
        {
            SongList = new List<SongShortInfo>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int ArtistId { get; set; }
        public IList<SongShortInfo> SongList { get; set; }
    }

    public class SongShortInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
