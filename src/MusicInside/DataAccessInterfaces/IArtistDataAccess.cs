using MusicInside.Models;
using MusicInside.ModelView;
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

        /// <summary>
        /// Retrieve all the song composed by an artist
        /// </summary>
        /// <param name="id">The identifier of the artist</param>
        /// <returns>A list of Song</returns>
        List<Song> GetListSongOfArtist(int id);

        /// <summary>
        /// Return all the song in database formatted as model want
        /// </summary>
        /// <returns>A list of ArtistRowViewModel</returns>
        List<ArtistRowViewModel> GetAll();
    }
}
