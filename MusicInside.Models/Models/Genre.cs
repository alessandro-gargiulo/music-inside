﻿using MusicInside.Models.AssociationClasses;
using System.Collections.Generic;

namespace MusicInside.Models.Models
{
    public class Genre
    {
        #region Properties
        public int Id { get; set; }
        public string Description { get; set; }
        #endregion

        #region Navigation Properties
        public IList<SongGenre> Songs { get; set; }
        #endregion
    }
}
