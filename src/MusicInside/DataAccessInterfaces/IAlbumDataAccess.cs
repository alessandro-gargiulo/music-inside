using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        List<Song> GetListSongById(int id);

        List<Album> GetListAlbumByArtistId(int id);

        /// <summary>
        /// Get the file of a cover
        /// </summary>
        /// <param name="id">The identifier of the album cover file</param>
        /// <returns>Representation of the image in byte array</returns>
        byte[] GetCoverFile(int id);
    }
}
