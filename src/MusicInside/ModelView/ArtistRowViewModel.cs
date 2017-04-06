using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.ModelView
{
    public class ArtistRowViewModel
    {
        public int ArtistId { get; set; }
        public string ArtName { get; set; }
        public int NumberSong { get; set; }

        public static ArtistRowViewModel Fill(SqlDataReader reader)
        {
            ArtistRowViewModel obj = new ArtistRowViewModel
            {
                 ArtistId = (int)reader["ArtistId"],
                 ArtName = (string)reader["ArtName"],
                 NumberSong = (int)reader["NumSong"]
            };
            return obj;
        }
    }
}
