using MusicInside.Models;
using System.Collections.Generic;

namespace MusicInside.DataAccessInterfaces
{
    public interface IGenreDataAccess
    {
        /// <summary>
        /// Retrieve genre details of a particular song
        /// </summary>
        /// <param name="id">Song identifier</param>
        /// <returns>A list of Genre</returns>
        List<Genre> GetGenresBySongId(int id);
    }
}
