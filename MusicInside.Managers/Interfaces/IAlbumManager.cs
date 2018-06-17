using MusicInside.Managers.Entities;
using System.Collections.Generic;

namespace MusicInside.Managers.Interfaces
{
    public interface IAlbumManager
    {
        /// <summary>
        /// Retrieve all data from database
        /// </summary>
        /// <returns>A list of all the albums</returns>
        List<EAlbum> GetAll();
        /// <summary>
        /// Retrieve a single album from database
        /// </summary>
        /// <param name="id">The identifier of the album</param>
        /// <returns>A single album</returns>
        EAlbum GetAlbumById(int id);
        /// <summary>
        /// Retrieve all the song contained into the album
        /// </summary>
        /// <param name="id">The identifier of the album</param>
        /// <returns>A list of all the songs contained into the album</returns>
        List<ESong> GetSongsInAlbum(int id);
        /// <summary>
        /// Retrieve the cover file for the album
        /// </summary>
        /// <param name="id">The identifier of the album</param>
        /// <returns>The byte file represented the album cover</returns>
        byte[] GetCoverFile(int id);
        /// <summary>
        /// Calculate how many song are contained into the album
        /// </summary>
        /// <param name="id">The identifier of the album</param>
        /// <returns>Number of song contained into the album</returns>
        int GetNumberOfSongs(int id);
    }
}
