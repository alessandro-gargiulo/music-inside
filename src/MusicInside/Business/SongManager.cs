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
using MusicInside.Exceptions;

namespace MusicInside.Business
{
    public class SongManager : ISongManager
    {
        private readonly ISongDataAccess _songDataAccess;
        private readonly IAlbumDataAccess _albumDataAccess;
        private readonly IArtistDataAccess _artistDataAccess;
        private readonly IGenreDataAccess _genreDataAccess;
        private readonly IStatisticDataAccess _statisticDataAccess;
        private readonly ILog _logger;

        public SongManager(ISongDataAccess songDataAccess, IAlbumDataAccess albumDataAccess, IArtistDataAccess artistDataAccess, IGenreDataAccess genreDataAccess, IStatisticDataAccess statistiDataAccess, ILog logger) {
            _songDataAccess = songDataAccess;
            _albumDataAccess = albumDataAccess;
            _artistDataAccess = artistDataAccess;
            _genreDataAccess = genreDataAccess;
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
                // Prepairing data
                Song song = _songDataAccess.GetSongById(id);
                Album album = _albumDataAccess.GetAlbumById(song.AlbumId.GetValueOrDefault());
                Artist artist = _artistDataAccess.GetArtistById(album.ArtistId);
                List<Genre> genres = _genreDataAccess.GetGenresBySongId(id);
                string genreLabel = String.Join(", ", genres.Select(des => des.Description).ToArray());
                Statistic stat = _statisticDataAccess.GetStatisticBySongId(id);

                // Fill the object
                sdvm.ArtistLabel = artist.ArtName;
                sdvm.TitleLabel = song.Title;
                sdvm.AlbumLabel = album.Title;
                sdvm.Year = song.Year;
                sdvm.TrackNo = song.TrackNo;
                sdvm.GenreLabel = genreLabel;
                sdvm.LastPlay = stat.LastPlay;
                sdvm.NumOfPlays = stat.NumPlay;
            }
            catch(InvalidIdException iiex)
            {
                _logger.Error("SongManager | GetDetailOfSong: Invalid id exception, throws at the top level");
                throw iiex;
            }
            catch(EntryNotPresentException enpex)
            {
                _logger.Error("SongManager | GetDetailOfSong: entry not present exception, throws at the top level");
                throw enpex;
            }
            catch(Exception ex)
            {
                _logger.Error("SongManager | GetDetailOfSong: A generic error occurred " + ex.Message);
            }

            return sdvm;
        }
    }
}
