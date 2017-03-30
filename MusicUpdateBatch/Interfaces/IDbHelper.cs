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

        /// <summary>
        /// Insert an artist or retrieve his id if he already exist in the database
        /// </summary>
        /// <param name="songTag">Tag Id3v2 of the song</param>
        /// <returns>The identifier of inserted/founded artist if success, -1 otherwise</returns>
        int TryToInsertArtist(TagLib.Tag songTag);

        /// <summary>
        /// Insert an album or retrieve its id if it already exist in the database
        /// </summary>
        /// <param name="songTag">Tag Id3v2 of the song</param>
        /// <param name="artistId">The identifier of the artist</param>
        /// <returns>The identifier of inserted/founded album if success, -1 otherwise</returns>
        int TryToInsertAlbumForArtist(TagLib.Tag songTag, int artistId);
    }
}
