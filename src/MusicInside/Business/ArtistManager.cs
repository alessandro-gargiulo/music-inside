using log4net;
using MusicInside.DataAccessInterfaces;
using MusicInside.Exceptions;
using MusicInside.ManagerInterfaces;
using MusicInside.Models;
using MusicInside.ModelView;
using System;
using System.Collections.Generic;

namespace MusicInside.Business
{
    public class ArtistManager : IArtistManager
    {
        private readonly IArtistDataAccess _artistDataAccess;
        private readonly IAlbumDataAccess _albumDataAccess;
        private readonly IFeaturingDataAccess _featuringDataAccess;
        private readonly ISongDataAccess _songDataAccess;
        private readonly ILog _logger;

        public ArtistManager(IArtistDataAccess artistDataAccess, IAlbumDataAccess albumDataAccess, IFeaturingDataAccess featuringDataAccess, ISongDataAccess songDataAccess, ILog logger)
        {
            _artistDataAccess = artistDataAccess;
            _albumDataAccess = albumDataAccess;
            _featuringDataAccess = featuringDataAccess;
            _songDataAccess = songDataAccess;
            _logger = logger;
        }

        public List<ArtistRowViewModel> GetAllTable()
        {
            List<ArtistRowViewModel> artists = new List<ArtistRowViewModel>();
            try
            {
                artists = _artistDataAccess.sp_GetAll();
                return artists;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("ArtistManager | GetAllTable: A generic error occurred [{0}]", ex.Message);
                throw ex;
            }
        }

        public ArtistDetailViewModel GetDetailById(int id)
        {
            ArtistDetailViewModel advm = new ArtistDetailViewModel();
            try
            {
                // Retrieving data
                Artist artist = _artistDataAccess.GetArtistById(id);
                List<Song> songs = _artistDataAccess.GetListSongOfArtist(id);
                List <Album> albums = _albumDataAccess.GetListAlbumByArtistId(id);
                // Fill the object
                advm.ArtistId = artist.ID;
                advm.ArtName = artist.ArtName;
                advm.BirthDate = artist.BirthYear;

                foreach (Song sng in songs)
                {
                    advm.SongInfos.Add(new ShortInfoViewModel
                    {
                        ID = sng.ID,
                        Title = sng.Title
                    });
                }
                foreach (Album albm in albums)
                {
                    advm.AlbumInfos.Add(new ShortInfoViewModel
                    {
                        ID = albm.ID,
                        Title = albm.Title
                    });
                }
            }
            catch (InvalidIdException iiex)
            {
                _logger.ErrorFormat("ArtistManager | GetDetailById: Invalid id exception, throws at the top level");
                throw iiex;
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("ArtistManager | GetDetailById: entry not present exception, throws at the top level");
                throw enpex;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("ArtistManager | GetDetailById: A generic error occurred [{0}]", ex.Message);
                throw ex;
            }
            return advm;
        }
    }
}
