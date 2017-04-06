using log4net;
using Microsoft.Extensions.Configuration;
using MusicInside.DataAccessInterfaces;
using MusicInside.Exceptions;
using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicInside.ModelView;
using System.Data.SqlClient;

namespace MusicInside.DataAccess
{
    public class ArtistDataAccess : BaseDataAccess, IArtistDataAccess
    {
        public ArtistDataAccess(SongDBContext context, IConfiguration conf, ILog logger) : base(context, conf, logger) { }

        public List<ArtistRowViewModel> GetAll()
        {
            List<ArtistRowViewModel> artists = new List<ArtistRowViewModel>();
            try
            {
                SqlConnection _connection = new SqlConnection(_connString);
                SqlCommand _cmd = new SqlCommand();
                SqlDataReader _reader;

                _cmd.CommandText = "sp_getAllArtist";
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Connection = _connection;

                _connection.Open();
                _reader = _cmd.ExecuteReader();
                while (_reader.Read())
                {
                    artists.Add(ArtistRowViewModel.Fill(_reader));
                }
                _connection.Close();
            }
            catch (SqlException sqlex)
            {
                _logger.Error("ArtistDataAccess | GetAllSong: SQL problem have occurred: " + sqlex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("ArtistDataAccess | GetAllSong: A generic problem have occurred: " + ex.Message);
            }
            return artists;
        }

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
