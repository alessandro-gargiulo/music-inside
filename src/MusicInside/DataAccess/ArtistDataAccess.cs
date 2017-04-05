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
    public class ArtistDataAccess : BaseDataAccess, IArtistDataAccess
    {
        public ArtistDataAccess(SongDBContext context, IConfiguration conf, ILog logger) : base(context, conf, logger) { }

        public Artist GetArtistById(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid artist id value. Value must be non-negative");
            Artist artist = null;
            try
            {
                artist = _db.Artists.Where(x => x.ID == id).FirstOrDefault();
                if (artist == null)
                {
                    throw new EntryNotPresentException("Can't found an artist with chosen id");
                }
            }
            catch (ArgumentNullException anex)
            {
                _logger.Error("ArtistDataAccess | GetArtistById: Cannot execute query with null argument: " + anex.Message);
            }
            return artist;
        }

        public List<Song> GetListSongOfArtist(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid artist id value. Value must be non-negative");
            List<Song> songs = new List<Song>();
            try
            {
                //songs = _db.Songs
                if (songs.Count() == 0)
                {
                    throw new EntryNotPresentException("Can't found a list of song with chosen id");
                }
            }
            catch (ArgumentNullException anex)
            {
                _logger.Error("ArtistDataAccess | GetListSongOfArtist: Cannot execute query with null argument: " + anex.Message);
            }
            return songs;
        }
    }
}
