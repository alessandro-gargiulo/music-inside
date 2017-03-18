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
        private readonly IAlbumDataAccess _albumDataAccess;
        private readonly IArtistDataAccess _artistDataAccess;
        private readonly ILog _logger;

        public SongManager(ISongDataAccess songDataAccess, IAlbumDataAccess albumDataAccess, IArtistDataAccess artistDataAccess, ILog logger) {
            _songDataAccess = songDataAccess;
            _albumDataAccess = albumDataAccess;
            _artistDataAccess = artistDataAccess;
            _logger = logger;
        }

        public List<SongRowViewModel> GetAllSongs()
        {
            List<SongRowViewModel> songs = _songDataAccess.GetAllSong();
            return songs;
        }

        public SongDetailViewModel GetDetailOfSong(int id)
        {
            SongDetailViewModel sdvm = new SongDetailViewModel();
            try
            {
                Song song = _songDataAccess.GetSongById(id);
                Album album = _albumDataAccess.GetAlbumById(song.AlbumId);
                Artist artist = _artistDataAccess.GetArtistById(album.ArtistId);
            }
            catch(Exception ex)
            {

            }

            return sdvm;
        }
    }
}
