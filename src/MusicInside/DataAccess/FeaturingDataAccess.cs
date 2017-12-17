using log4net;
using Microsoft.Extensions.Configuration;
using MusicInside.DataAccessInterfaces;
using MusicInside.Exceptions;
using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicInside.DataAccess
{
    public class FeaturingDataAccess : BaseDataAccess, IFeaturingDataAccess
    {
        public FeaturingDataAccess(SongDBContext context, IConfiguration conf, ILog logger) : base(context, conf, logger) { }

        public List<int> GetBothPrincipalAndFeatsArtistId(int songId)
        {
            if (songId < 0) throw new InvalidIdException("Invalid song id value. Value must be non-negative");
            List<int> ids = new List<int>();
            try
            {
                ids = _db.Featurings.Where(x => x.SongId == songId).Select(w => w.ArtistId).ToList();
            }
            catch (ArgumentNullException anex)
            {
                _logger.ErrorFormat("FeaturingDataAccess | GetBothPrincipalAndFeatsArtistId: Cannot execute query with null argument [{0}]", anex.Message);
            }
            return ids;
        }

        public List<int> GetOnlyFeatsArtistId(int songId)
        {
            if (songId < 0) throw new InvalidIdException("Invalid song id value. Value must be non-negative");
            List<int> ids = new List<int>();
            try
            {
                ids = _db.Featurings.Where(x => x.SongId == songId).Where(y => y.IsPrincipalArtist == false).Select(w => w.ArtistId).ToList();
            }
            catch (ArgumentNullException anex)
            {
                _logger.ErrorFormat("FeaturingDataAccess | GetOnlyFeatsArtistId: Cannot execute query with null argument [{0}]", anex.Message);
            }
            return ids;
        }

        public int GetPrincipalArtistId(int songId)
        {
            if (songId < 0) throw new InvalidIdException("Invalid song id value. Value must be non-negative");
            int id = 0;
            try
            {
                id = _db.Featurings.Where(x => x.SongId == songId).Where(y => y.IsPrincipalArtist == true).Select(w => w.ArtistId).SingleOrDefault();
            }
            catch (ArgumentNullException anex)
            {
                _logger.ErrorFormat("FeaturingDataAccess | GetPrincipalArtistId: Cannot execute query with null argument [{0}]", anex.Message);
            }
            return id;
        }

        public List<int> GetSongFeaturingOfArtistId(int artistId)
        {
            if (artistId < 0) throw new InvalidIdException("Invalid artist id value. Value must be non-negative");
            List<int> ids = new List<int>();
            try
            {
                ids = _db.Featurings.Where(x => x.ArtistId == artistId).Where(y => y.IsPrincipalArtist == false).Select(w => w.SongId).ToList();
            }
            catch (ArgumentNullException anex)
            {
                _logger.ErrorFormat("FeaturingDataAccess | GetSongFeaturingOfArtistId: Cannot execute query with null argument [{0}]", anex.Message);
            }
            return ids;
        }

        public List<int> GetBothSongAndFeaturingOfArtistId(int artistId)
        {
            if (artistId < 0) throw new InvalidIdException("Invalid artist id value. Value must be non-negative");
            List<int> ids = new List<int>();
            try
            {
                ids = _db.Featurings.Where(x => x.ArtistId == artistId).Select(w => w.SongId).ToList();
            }
            catch (ArgumentNullException anex)
            {
                _logger.ErrorFormat("FeaturingDataAccess | GetSongFeaturingOfArtistId: Cannot execute query with null argument [{0}]", anex.Message);
            }
            return ids;
        }
    }
}
