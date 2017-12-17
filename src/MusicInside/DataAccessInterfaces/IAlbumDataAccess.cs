using MusicInside.Models;
using MusicInside.ModelView;
using System.Collections.Generic;

namespace MusicInside.DataAccessInterfaces
{
    public interface IAlbumDataAccess
    {
        /// <summary>
        /// Get a particular album indexed by its id
        /// </summary>
        /// <param name="id">The identifier of the album</param>
        /// <returns>An object Album</returns>
        Album GetAlbumById(int id);

        /// <summary>
        /// Get all the song contained into an album
        /// </summary>
        /// <param name="id">The identifier of the album</param>
        /// <returns>A list of Song</returns>
        List<Song> GetListSongById(int id);

        /// <summary>
        /// Return all the album composed by an artist
        /// </summary>
        /// <param name="id">The identifier of the artist</param>
        /// <returns>A list of Album</returns>
        List<Album> GetListAlbumByArtistId(int id);

        /// <summary>
        /// Get the file of a cover
        /// </summary>
        /// <param name="id">The identifier of the album cover file</param>
        /// <returns>Representation of the image in byte array</returns>
        byte[] GetCoverFile(int id);

        /// <summary>
        /// Get the list of all albums in the database
        /// </summary>
        /// <returns>List of all album</returns>
        List<AlbumRowViewModel> GetAll();
    }
}
