using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.ModelView
{
    public class AlbumDetailViewModel
    {
        public AlbumDetailViewModel()
        {
            SongInfos = new List<SongShortInfo>();
        }

        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int AlbumCoverFileId { get; set; }
        public List<SongShortInfo> SongInfos { get; set; }

        public class SongShortInfo
        {
            public int ID { get; set; }
            public string Title { get; set; }
        }
    }
}
