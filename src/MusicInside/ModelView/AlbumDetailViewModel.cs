using System.Collections.Generic;

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
