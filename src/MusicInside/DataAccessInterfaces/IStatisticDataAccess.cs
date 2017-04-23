using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.DataAccessInterfaces
{
    public interface IStatisticDataAccess
    {
        /// <summary>
        /// Retrieve statistic data of a song identified by its id
        /// </summary>
        /// <param name="id">Song identifier</param>
        /// <returns>A statistic object</returns>
        Statistic GetStatisticBySongId(int id);

        /// <summary>
        /// Update the statistic for the song with given id. This function add the last played date and an hit to the counter
        /// </summary>
        /// <param name="id">The song identifier</param>
        void UpdateStatisticForSongId(int id);
    }
}
