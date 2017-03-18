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
    public class GenreDataAccess : BaseDataAccess, IGenreDataAccess
    {
        public GenreDataAccess(SongDBContext context, IConfiguration conf, ILog logger) : base(context, conf, logger) { }

        public List<Genre> GetGenresBySongId(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid song id");
            List<Genre> query = _db.SongGenres.Join(_db.Genres,
                x => x.GenreId,
                y => y.ID, (x, y) => new
                {
                    SG = x,
                    G = y
                }).Where(z => z.SG.SongId == id).Select(s => new Genre { ID = s.G.ID, Description = s.G.Description }).ToList();
            return query;
        }
    }
}
