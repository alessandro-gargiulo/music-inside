using Microsoft.Extensions.Configuration;
using MusicInside.DataAccessInterfaces;
using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicInside.ModelView;
using System.Data.SqlClient;

namespace MusicInside.DataAccess
{
    public class SongDataAccess : BaseDataAccess, ISongDataAccess
    {
        public SongDataAccess(SongDBContext context, IConfiguration conf) : base(context, conf) { }

        /// <summary>
        /// Call a stored procedure which return all the song from the database formatted as the view model.
        /// </summary>
        /// <returns>A list of songs formatted as the view model want</returns>
        public List<SongRowViewModel> getAllSong()
        {
            List<SongRowViewModel> songs = new List<SongRowViewModel>();
            try
            {
                SqlConnection _connection = new SqlConnection(connString);
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
            catch (Exception e)
            {
                
            }
            return songs;
        }
    }
}
