using System;
using System.Collections.Generic;
using System.Text;

namespace MusicUpdateBatch.Interfaces
{
    interface IDbHelper
    {
        /// <summary>
        /// Insert a song into the database
        /// </summary>
        /// <param name="songTag">Tag Id3v2 of the song</param>
        /// <returns>The identifier of inserted song if success, -1 otherwise</returns>
        int InsertSong(TagLib.Tag songTag);

        int TryToInsertArtist(TagLib.Tag songTag);
    }
}
