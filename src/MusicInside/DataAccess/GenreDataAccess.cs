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
    public class GenreDataAccess : BaseDataAccess, IGenreDataAccess
    {
        public GenreDataAccess(SongDBContext context, IConfiguration conf, ILog logger) : base(context, conf, logger) { }

        public List<Genre> GetGenresBySongId(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid song id");
            List<Genre> query = null;
            try
            {
                query = _db.SongGenres.Join(_db.Genres,
                    x => x.GenreId,
                    y => y.ID, (x, y) => new
                    {
                        SG = x,
                        G = y
                    })
                    .Where(z => z.SG.SongId == id)
                    .Select(s => new Genre { ID = s.G.ID, Description = s.G.Description })
                    .ToList();
                if (query == null) throw new EntryNotPresentException("Did not found song id into the database");
            }
            catch (ArgumentException anex)
            {
                _logger.ErrorFormat("GenreDataAccess | GenreDataAccess: Cannot execute query with null argument [{0}]", anex.Message);
            }
            return query;
        }
    }
}
