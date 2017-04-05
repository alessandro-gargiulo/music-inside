using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.DataAccessInterfaces
{
    public interface IArtistDataAccess
    {
        /// <summary>
        /// Get a particular artist indexed by its id
        /// </summary>
        /// <param name="id">The identifier of the artist</param>
        /// <returns>An object Artist</returns>
        Artist GetArtistById(int id);

        List<Song> GetListSongOfArtist(int id);
    }
}
