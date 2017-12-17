using MusicInside.Models;
using MusicInside.ModelView;
using System.Collections.Generic;

namespace MusicInside.DataAccessInterfaces
{
    public interface ISongDataAccess
    {
        /// <summary>
        /// Get all the songs from the database
        /// </summary>
        /// <returns>A list of songs formatted as the view model want</returns>
        List<SongRowViewModel> GetAll();

        /// <summary>
        /// Get all the song from database which starts with given letter
        /// </summary>
        /// <param name="letter">The first letter of song title</param>
        /// <returns>A list of songs formatted as the view model want</returns>
        List<SongRowViewModel> GetAllByLetter(string letter);

        /// <summary>
        /// Get a particular song indexed by its id
        /// </summary>
        /// <param name="id">The identifier of the song</param>
        /// <returns>An object Song</returns>
        Song GetById(int id);
    }
}
