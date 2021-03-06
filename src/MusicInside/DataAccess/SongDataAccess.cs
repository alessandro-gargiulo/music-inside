﻿using log4net;
using Microsoft.Extensions.Configuration;
using MusicInside.DataAccessInterfaces;
using MusicInside.Exceptions;
using MusicInside.Models;
using MusicInside.ModelView;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace MusicInside.DataAccess
{
    public class SongDataAccess : BaseDataAccess, ISongDataAccess
    {
        public SongDataAccess(SongDBContext context, IConfiguration conf, ILog logger) : base(context, conf, logger) { }

        public List<SongRowViewModel> sp_GetAll()
        {
            List<SongRowViewModel> songs = new List<SongRowViewModel>();
            try
            {
                SqlConnection _connection = new SqlConnection(_connString);
                SqlCommand _cmd = new SqlCommand();
                SqlDataReader _reader;

                _cmd.CommandText = "sp_getAllSong";
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Connection = _connection;

                _connection.Open();
                _reader = _cmd.ExecuteReader();
                while (_reader.Read())
                {
                    songs.Add(SongRowViewModel.Fill(_reader));
                }
                _connection.Close();
            }
            catch (SqlException sqlex)
            {
                _logger.ErrorFormat("SongDataAccess | GetAll: SQL problem have occurred [{0}]", sqlex.Message);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("SongDataAccess | GetAll: A generic problem have occurred [{0}]", ex.Message);
            }
            return songs;
        }

        public List<SongRowViewModel> sp_GetAllByLetter(string letter)
        {
            List<SongRowViewModel> songs = new List<SongRowViewModel>();
            try
            {
                SqlConnection _connection = new SqlConnection(_connString);
                SqlCommand _cmd = new SqlCommand();
                SqlDataReader _reader;

                var storedParameter = new SqlParameter("@letter", letter);

                _cmd.CommandText = "sp_GetAllSongStartsWith";
                _cmd.CommandType = System.Data.CommandType.StoredProcedure;
                _cmd.Parameters.Add(storedParameter);
                _cmd.Connection = _connection;

                _connection.Open();
                _reader = _cmd.ExecuteReader();
                while (_reader.Read())
                {
                    songs.Add(SongRowViewModel.Fill(_reader));
                }
                _connection.Close();
            }
            catch (SqlException sqlex)
            {
                _logger.ErrorFormat("SongDataAccess | GetAllByLetter: SQL problem have occurred [{0}]", sqlex.Message);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("SongDataAccess | GetAllByLetter: A generic problem have occurred [{0}]", ex.Message);
            }
            return songs;
        }

        public Song GetById(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid song id value. Value must be non-negative");
            Song song = null;
            try
            {
                song = _db.Songs.Where(x => x.ID == id).FirstOrDefault();
                if(song == null)
                {
                    throw new EntryNotPresentException("Can't found a song with chosen id");
                }
            }
            catch(ArgumentNullException anex)
            {
                _logger.ErrorFormat("SongDataAccess | GetById: Cannot execute query with null argument [{0}]", anex.Message);
            }
            return song;
        }
    }
}
