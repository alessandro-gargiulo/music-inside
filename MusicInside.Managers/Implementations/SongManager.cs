using log4net;
using MusicInside.Managers.Entities;
using MusicInside.Managers.Interfaces;
using MusicInside.Models.Context;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicInside.Managers.Implementations
{
    public class SongManager : ISongManager
    {
        private MusicInsideDbContext _dbContext;
        private ILog _logger;

        public void AddPlayToSongId(int id, DateTime when)
        {
            throw new NotImplementedException();
        }

        public List<ESong> GetAll()
        {
            throw new NotImplementedException();
        }

        public byte[] GetFileBytes(int id)
        {
            throw new NotImplementedException();
        }

        public List<EGenre> GetGenresForSong(int id)
        {
            throw new NotImplementedException();
        }

        public List<ESong> GetMatchingRegex(Regex regex)
        {
            throw new NotImplementedException();
        }

        public List<EMoment> GetMomentsForSong(int id)
        {
            throw new NotImplementedException();
        }

        public ESong GetSongById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ESong> GetStartingWithString(string initialString)
        {
            throw new NotImplementedException();
        }
    }
}
