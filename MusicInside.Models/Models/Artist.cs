using MusicInside.Models.AssociationClasses;
using System.Collections.Generic;

namespace MusicInside.Models.Models
{
    public class Artist
    {
        #region Properties
        public int Id { get; set; }
        public string ArtName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        public bool IsBand { get; set; }
        #endregion

        #region Navigation Properties
        public IList<SongArtist> Songs { get; set; }
        #endregion

    }
}
