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

        /// <summary>
        /// Update the field AlbumId of a Song identified by its id
        /// </summary>
        /// <param name="songId">Song identifier</param>
        /// <param name="albumId">Album identifier</param>
        void UpdateSongAlbum(int songId, int albumId);

        /// <summary>
        /// Insert a genre or retrieve its id if it already exist in the database
        /// </summary>
        /// <param name="songTag">Tag Id3v2 of the song</param>
        /// <param name="songId">Song identifier</param>
        void TryToInsertGenre(TagLib.Tag songTag, int songId);

        /// <summary>
        /// Insert a file entry in the database
        /// </summary>
        /// <param name="folder">Name of the folder</param>
        /// <param name="fileName">File name</param>
        /// <param name="songId">Song identifier</param>
        void InsertPhysicalFile(string folder, string fileName, int songId);

        /// <summary>
        /// Retrieve the cover picture from the tag and save both on file-system and database
        /// </summary>
        /// <param name="songTag">Tag Id3v2 of the song</param>
        /// <param name="albumId">Album identifier</param>
        void KeepCoverFile(TagLib.Tag songTag, int albumId);
    }
}
