using MusicInside.Models;
using MusicInside.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.DataAccessInterfaces
{
    public interface ISongDataAccess
    {
        /// <summary>
        /// Get all the songs from the database
        /// </summary>
        /// <returns>A list of songs formatted as the view model want</returns>
        List<SongRowViewModel> GetAllSong();

        /// <summary>
        /// Get a particular song indexed by its id
        /// </summary>
        /// <param name="id">The identifier of the song</param>
        /// <returns>An object Song</returns>
        Song GetSongById(int id);
    }
}
