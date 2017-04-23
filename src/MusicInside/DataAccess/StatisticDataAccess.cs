using log4net;
using Microsoft.Extensions.Configuration;
using MusicInside.DataAccessInterfaces;
using MusicInside.Exceptions;
using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.DataAccess
{
    public class StatisticDataAccess : BaseDataAccess, IStatisticDataAccess
    {
        public StatisticDataAccess(SongDBContext context, IConfiguration conf, ILog logger) : base(context, conf, logger) { }

        public Statistic GetStatisticBySongId(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid song id value. Value must be non-negative");
            Statistic statistic = null;
            try
            {
                statistic = _db.Statistics.Where(x => x.SongId == id).FirstOrDefault();
                if (statistic == null)
                {
                    throw new EntryNotPresentException("Can't found a statistic with chosen song-id");
                }
            }
            catch (ArgumentNullException anex)
            {
                _logger.ErrorFormat("StatisticDataAccess | GetStatisticBySongId: Cannot execute query with null argument [{0}]", anex.Message);
            }
            return statistic;
        }

        public void UpdateStatisticForSongId(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid song id value. Value must be non-negative");

            try
            {
                Statistic statistic = _db.Statistics.Where(x => x.SongId == id).FirstOrDefault();
                if (statistic == null)
                {
                    throw new EntryNotPresentException("Can't found a statistic with chosen song-id");
                }
                else
                {
                    statistic.LastPlay = DateTime.Now;
                    statistic.NumPlay += 1;
                    _db.Statistics.Update(statistic);
                    _db.SaveChanges();
                }
            }
            catch (ArgumentNullException anex)
            {
                _logger.ErrorFormat("StatisticDataAccess | UpdateStatisticForSongId: Cannot execute query with null argument [{0}]", anex.Message);
            }
        }
    }
}
