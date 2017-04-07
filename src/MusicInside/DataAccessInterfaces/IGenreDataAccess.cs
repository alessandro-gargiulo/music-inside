using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
