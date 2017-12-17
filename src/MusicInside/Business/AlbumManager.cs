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
    public class AlbumManager : IAlbumManager
    {
        private readonly IAlbumDataAccess _albumDataAccess;
        private readonly IArtistDataAccess _artistDataAccess;
        private readonly ILog _logger;

        public AlbumManager(IAlbumDataAccess albumDataAccess, IArtistDataAccess artistDataAccess, ILog logger)
        {
            _albumDataAccess = albumDataAccess;
            _artistDataAccess = artistDataAccess;
            _logger = logger;
        }

        public AlbumDetailViewModel GetDetailById(int id)
        {
            AlbumDetailViewModel advm = new AlbumDetailViewModel();
            try
            {
                // Retrieving data
                Album album = _albumDataAccess.GetAlbumById(id);
                Artist artist = _artistDataAccess.GetArtistById(album.ArtistId);
                List<Song> songs = _albumDataAccess.GetListSongById(id);
                // Fill the object
                advm.AlbumId = album.ID;
                advm.Title = album.Title;
                advm.Artist = artist.ArtName;
                advm.AlbumCoverFileId = album.FileId.GetValueOrDefault();
                foreach (Song sng in songs)
                {
                    advm.SongInfos.Add(new ShortInfoViewModel
                    {
                        ID = sng.ID,
                        Title = sng.Title
                    });
                }
            }
            catch (InvalidIdException iiex)
            {
                _logger.ErrorFormat("AlbumManager | GetDetailById: Invalid id exception, throws at the top level");
                throw iiex;
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("AlbumManager | GetDetailById: entry not present exception, throws at the top level");
                throw enpex;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("AlbumManager | GetDetailById: A generic error occurred [{0}]", ex.Message);
                throw ex;
            }
            return advm;
        }

        public byte[] GetAlbumCoverFile(int id)
        {
            byte[] data = null;
            try
            {
                data = _albumDataAccess.GetCoverFile(id);
            }
            catch (InvalidIdException iiex)
            {
                _logger.ErrorFormat("AlbumManager | GetAlbumCoverFile: Invalid id exception, throws at the top level");
                throw iiex;
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("AlbumManager | GetAlbumCoverFile: entry not present exception, throws at the top level");
                throw enpex;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("AlbumManager | GetAlbumCoverFile: A generic error occurred [{0}]", ex.Message);
            }
            return data;
        }

        public List<AlbumRowViewModel> GetAllTable()
        {
            List<AlbumRowViewModel> albums = new List<AlbumRowViewModel>();
            try
            {
                albums = _albumDataAccess.GetAll();
                return albums;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("AlbumManager | GetAllTable: A generic error occurred [{0}]", ex.Message);
                throw ex;
            }
        }
    }
}
