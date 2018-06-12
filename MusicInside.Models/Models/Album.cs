using System.Collections.Generic;

namespace MusicInside.Models.Models
{
    public class Album
    {
        #region Properties
        public int Id { get; set; }
        public string Title { get; set; }
        #endregion

        #region Navigation Properties
        public IList<Song> Songs { get; set; }

        public Cover Cover { get; set; }
        public int CoverId { get; set; }
        #endregion
    }
}
