using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.ModelView
{
    public class AlbumRowViewModel
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public int NumberSong { get; set; }
        public string ArtistName { get; set; }
        public int ArtistId { get; set; }

        public static AlbumRowViewModel Fill(SqlDataReader reader)
        {
            AlbumRowViewModel obj = new AlbumRowViewModel
            {
                AlbumId = (int)reader["AlbumId"],
                AlbumName = (string)reader["AlbumName"],
                NumberSong = (int)reader["NumberSong"],
                ArtistName = (string)reader["ArtistName"],
                ArtistId = (int)reader["ArtistId"]
            };
            return obj;
        }
    }
}
