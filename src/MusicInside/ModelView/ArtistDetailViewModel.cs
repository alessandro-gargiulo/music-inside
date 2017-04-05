using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.ModelView
{
    public class ArtistDetailViewModel
    {
        public ArtistDetailViewModel()
        {
            SongInfos = new List<ShortInfoViewModel>();
            AlbumInfos = new List<ShortInfoViewModel>();
        }

        public int ArtistId { get; set; }
        public string ArtName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }
        public List<ShortInfoViewModel> SongInfos { get; set; }
        public List<ShortInfoViewModel> AlbumInfos { get; set; }
    }
}
