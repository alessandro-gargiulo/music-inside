using log4net;
using MusicInside.DataAccessInterfaces;
using MusicInside.Exceptions;
using MusicInside.ManagerInterfaces;
using MusicInside.Models;
using MusicInside.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicInside.Business
{
    public class SongManager : ISongManager
    {
        private readonly ISongDataAccess _songDataAccess;
        private readonly IAlbumDataAccess _albumDataAccess;
        private readonly IArtistDataAccess _artistDataAccess;
        private readonly IGenreDataAccess _genreDataAccess;
        private readonly IStatisticDataAccess _statisticDataAccess;
        private readonly IFileDataAccess _fileDataAccess;
        private readonly IFeaturingDataAccess _featuringDataAccess;
        private readonly ILog _logger;

        public SongManager(ISongDataAccess songDataAccess, 
                            IAlbumDataAccess albumDataAccess,
                            IArtistDataAccess artistDataAccess,
                            IGenreDataAccess genreDataAccess,
                            IStatisticDataAccess statisticDataAccess,
                            IFileDataAccess fileDataAccess,
                            IFeaturingDataAccess featuringDataAccess,
                            ILog logger) {
            _songDataAccess = songDataAccess;
            _albumDataAccess = albumDataAccess;
            _artistDataAccess = artistDataAccess;
            _genreDataAccess = genreDataAccess;
            _statisticDataAccess = statisticDataAccess;
            _fileDataAccess = fileDataAccess;
            _featuringDataAccess = featuringDataAccess;
            _logger = logger;
        }

        public List<SongRowViewModel> GetAllTable()
        {
            List<SongRowViewModel> songs = new List<SongRowViewModel>();
            try
            {
                songs = _songDataAccess.GetAll();
                return songs;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("SongManager | GetAllTable: A generic error occurred [{0}]", ex.Message);
                throw ex;
            }
        }

        public SongDetailViewModel GetDetailById(int id)
        {
            SongDetailViewModel sdvm = new SongDetailViewModel();
            try
            {
                // Prepairing data
                Song song = _songDataAccess.GetById(id);
                Album album = _albumDataAccess.GetAlbumById(song.AlbumId.GetValueOrDefault());
                Artist artist = _artistDataAccess.GetArtistById(album.ArtistId);
                List<int> featsArtistIdList = _featuringDataAccess.GetOnlyFeatsArtistId(id);
                string artistLabel = artist.ArtName;
                artistLabel = featsArtistIdList.Count != 0 ? artistLabel + " Feat. " : artistLabel;
                foreach(int featId in featsArtistIdList)
                {
                    Artist featArtist = _artistDataAccess.GetArtistById(featId);
                    artistLabel = artistLabel + featArtist.ArtName + ",";
                }
                artistLabel = featsArtistIdList.Count != 0 ? artistLabel.Substring(0, artistLabel.Length - 1) : artistLabel;
                List <Genre> genres = _genreDataAccess.GetGenresBySongId(id);
                string genreLabel = String.Join(", ", genres.Select(des => des.Description).ToArray());
                Statistic stat = _statisticDataAccess.GetStatisticBySongId(id);

                // Fill the object
                sdvm.SongId = id;
                sdvm.ArtistLabel = artistLabel;
                sdvm.TitleLabel = song.Title;
                sdvm.AlbumLabel = album.Title;
                sdvm.Year = song.Year;
                sdvm.TrackNo = song.TrackNo;
                sdvm.GenreLabel = genreLabel;
                sdvm.LastPlay = stat.LastPlay;
                sdvm.NumOfPlays = stat.NumPlay;
                sdvm.AlbumCoverFileId = album.FileId.GetValueOrDefault();
            }
            catch(InvalidIdException iiex)
            {
                _logger.ErrorFormat("SongManager | GetDetailOfSong: Invalid id exception, throws at the top level");
                throw iiex;
            }
            catch(EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("SongManager | GetDetailOfSong: entry not present exception, throws at the top level");
                throw enpex;
            }
            catch(Exception ex)
            {
                _logger.ErrorFormat("SongManager | GetDetailOfSong: A generic error occurred [{0}]", ex.Message);
                throw ex;
            }

            return sdvm;
        }

        public byte[] GetFileBytesById(int id)
        {
            byte[] bytes = null;
            try
            {
                bytes = _fileDataAccess.GetFileBytesById(id);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("SongManager | GetFileBytesById: Exception occurred when retrieving byte of songId={0} [{1}]", id, ex.Message);
            }
            return bytes;
        }

        public void AddPlayToSongId(int id)
        {
            try
            {
                _statisticDataAccess.UpdateStatisticForSongId(id);
            }
            catch (InvalidIdException iiex)
            {
                _logger.ErrorFormat("SongManager | AddPlayToSongId: Invalid id exception, throws at the top level");
                throw iiex;
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("SongManager | AddPlayToSongId: entry not present exception, throws at the top level");
                throw enpex;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("SongManager | AddPlayToSongId: A generic error occurred [{0}]", ex.Message);
                throw ex;
            }
        }

        public List<SongRowViewModel> GetTablePart(string letter)
        {
            List<SongRowViewModel> songs = new List<SongRowViewModel>();
            try
            {
                songs = _songDataAccess.GetAllByLetter(letter);
                return songs;
            }
            catch(Exception ex)
            {
                _logger.ErrorFormat("SongManager | GetTablePart: A generic error occurred [{0}]", ex.Message);
                throw ex;
            }
        }
    }
}
