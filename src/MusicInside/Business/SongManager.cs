using MusicInside.DataAccess;
using MusicInside.ManagerInterfaces;
using MusicInside.Models;
using MusicInside.ModelView;
using MusicInside.DataAccessInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.Business
{
    public class SongManager : ISongManager
    {
        private readonly ISongDataAccess _songDataAccess;

        public SongManager(ISongDataAccess dataAccess) {
            _songDataAccess = dataAccess;
        }

        public List<SongRowViewModel> getAllSongs()
        {
            List<SongRowViewModel> songs = _songDataAccess.getAllSong();
            return songs;
        }
    }
}
