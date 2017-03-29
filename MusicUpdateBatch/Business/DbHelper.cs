using MusicInside.Models;
using MusicUpdateBatch.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using TagLib;

namespace MusicUpdateBatch.Business
{
    public class DbHelper : IDisposable, IDbHelper
    {
        private readonly SongDBContext _context;
        private readonly log4net.ILog _logger;

        public DbHelper(log4net.ILog log, SongDBContext context)
        {
            _context = context;
            _logger = log;
        }

        public int InsertSong(Tag songTag)
        {
            try
            {
                Song newSong = new Song();
                newSong.Title = songTag.Title;
                newSong.TrackNo = (int)songTag.Track;
                newSong.Year = (int)songTag.Year;
                _context.Songs.Add(newSong);
                _context.SaveChanges();
                return newSong.ID;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("DbHelper | InsertSong: Can't insert song with title <{0}> due to exception [{1}]", songTag.Title, ex.Message);
                return -1;
            }
        }

        public int TryToInsertArtist(Tag songTag)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
