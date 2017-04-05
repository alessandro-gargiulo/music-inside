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
            SongInfos = new List<ShortInfoViewModel>();
        }

        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int AlbumCoverFileId { get; set; }
        public List<ShortInfoViewModel> SongInfos { get; set; }
    }
}
