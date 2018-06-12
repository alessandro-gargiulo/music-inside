using MusicInside.Models.Models;

namespace MusicInside.Models.AssociationClasses
{
    public class SongGenre
    {
        #region Navigation Properties
        public Song Song { get; set; }
        public int SongId { get; set; }

        public Genre Genre { get; set; }
        public int GenreId { get; set; }
        #endregion
    }
}
