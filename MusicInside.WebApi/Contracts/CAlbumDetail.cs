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
        public IList<SongShortInfo> SongList { get; set; }
    }

    public class SongShortInfo
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }
}
