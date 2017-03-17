using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.Data
{
    public class DbInitializer
    {
        public static void Initialize(SongDBContext context)
        {
            context.Database.EnsureCreated();
            if (context.Songs.Any())
            {
                return;
            }

            var songs = new Song[]
            {
                new Song {Title="Albachiara" },
                new Song { Title = "Bella ciao" }
            };
            foreach(Song song in songs)
            {
                context.Songs.Add(song);
            }
            context.SaveChanges();
        }
    }
}
