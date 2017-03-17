using MusicInside.DataAccess;
using MusicInside.ManagerInterfaces;
using MusicInside.Models;
using MusicInside.ModelView;
using MusicInside.DataAccessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;

namespace MusicInside.Business
{
    public class SongManager : ISongManager
    {
        private readonly ISongDataAccess _songDataAccess;
        private readonly ILog _logger;

        public SongManager(ISongDataAccess dataAccess, ILog logger) {
            _songDataAccess = dataAccess;
            _logger = logger;
        }

        public List<SongRowViewModel> getAllSongs()
        {
            List<SongRowViewModel> songs = _songDataAccess.getAllSong();
            return songs;
        }
    }
}
