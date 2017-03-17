using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.ModelView
{
    public class SongRowViewModel
    {
        public string TitleLabel { get; set; }
        public string ArtistLabel { get; set; }
        public string AlbumLabel { get; set; }
        public string GenreLabel { get; set; }
        public int YearNumber { get; set; }
        public int SongId { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }

        public static SongRowViewModel Fill(SqlDataReader reader)
        {
            SongRowViewModel obj = new SongRowViewModel
            {
                TitleLabel = (string)reader["TitleLabel"],
                ArtistLabel = (string)reader["ArtistLabel"],
                AlbumLabel = (string)reader["AlbumLabel"],
                GenreLabel = (string)reader["GenreLabel"],
                YearNumber = (int)reader["YearNumber"],
                SongId = (int)reader["SongId"],
                AlbumId = (int)reader["AlbumId"],
                ArtistId = (int)reader["ArtistId"]
            };
            return obj;
        }
    }
}
