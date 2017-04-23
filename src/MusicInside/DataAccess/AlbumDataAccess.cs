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
    public class AlbumDataAccess : BaseDataAccess, IAlbumDataAccess
    {
        public AlbumDataAccess(SongDBContext context, IConfiguration conf, ILog logger) : base(context, conf, logger) { }

        public Album GetAlbumById(int id)
        {
            {
                if (id < 0) throw new InvalidIdException("Invalid album id value. Value must be non-negative");
                Album album = null;
                try
                {
                    album = _db.Albums.Where(x => x.ID == id).FirstOrDefault();
                    if (album == null)
                    {
                        throw new EntryNotPresentException("Can't found an album with chosen id");
                    }
                }
                catch (ArgumentNullException anex)
                {
                    _logger.ErrorFormat("AlbumDataAccess | GetAlbumById: Cannot execute query with null argument [{0}]", anex.Message);
                }
                return album;
            }
        }

        public List<Song> GetListSongById(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid album id value. Value must be non-negative");
            List<Song> songs = new List<Song>();
            try
            {
                songs = _db.Songs.Where(x => x.AlbumId == id).ToList();
                if (songs.Count() == 0)
                {
                    throw new EntryNotPresentException("Can't found a list of song with chosen id");
                }
            }
            catch (ArgumentNullException anex)
            {
                _logger.ErrorFormat("AlbumDataAccess | GetListSongById: Cannot execute query with null argument [{0}]", anex.Message);
            }
            return songs;
        }

        public List<Album> GetListAlbumByArtistId(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid album id value. Value must be non-negative");
            List<Album> albums = new List<Album>();
            try
            {
                albums = _db.Albums.Where(x => x.ArtistId == id).ToList();
            }
            catch (ArgumentNullException anex)
            {
                _logger.ErrorFormat("AlbumDataAccess | GetListAlbumByArtistId: Cannot execute query with null argument [{0}]", anex.Message);
            }
            return albums;
        }

        public byte[] GetCoverFile(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid album id value. Value must be non-negative");
            byte[] arrayByte = null;
            try
            {
                File file = _db.Files.Where(x => x.ID == id).FirstOrDefault();
                if(file == null)
                {
                    throw new EntryNotPresentException("Can't found an album cover with chosen id");
                }
                string path = System.IO.Path.Combine(_fileMusicRoot, file.Path, file.FileName + "." + file.Extension);
                arrayByte = System.IO.File.ReadAllBytes(path);
            }
            catch(System.IO.FileNotFoundException fnfex)
            {
                _logger.ErrorFormat("AlbumDataAccess | GetCoverFile: Cannot found file at given path [{0}]", fnfex.Message);
            }
            catch(Exception ex)
            {
                _logger.ErrorFormat("AlbumDataAccess | GetCoverFile: A generic exception occurred [{0}]", ex.Message);
            }
            return arrayByte;
        }

        public List<AlbumRowViewModel> GetAll()
        {
            List<AlbumRowViewModel> albums = new List<AlbumRowViewModel>();
            try
            {
                SqlConnection _connection = new SqlConnection(_connString);
                SqlCommand _cmd = new SqlCommand();
                SqlDataReader _reader;

                _cmd.CommandText = "sp_GetAllAlbum";
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Connection = _connection;

                _connection.Open();
                _reader = _cmd.ExecuteReader();
                while (_reader.Read())
                {
                    albums.Add(AlbumRowViewModel.Fill(_reader));
                }
                _connection.Close();
            }
            catch (SqlException sqlex)
            {
                _logger.ErrorFormat("AlbumDataAccess | GetAll: SQL problem have occurred [{0}]", sqlex.Message);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("AlbumDataAccess | GetAll: A generic problem have occurred [{0}]", ex.Message);
            }
            return albums;
        }
    }
}
