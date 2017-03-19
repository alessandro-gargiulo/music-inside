using log4net;
using MusicInside.DataAccessInterfaces;
using MusicInside.Exceptions;
using MusicInside.ManagerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.Business
{
    public class AlbumManager : IAlbumManager
    {
        private readonly IAlbumDataAccess _albumDataAccess;
        private readonly ILog _logger;

        public AlbumManager(IAlbumDataAccess dataAccess, ILog logger)
        {
            _albumDataAccess = dataAccess;
            _logger = logger;
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
                _logger.Error("AlbumManager | GetAlbumCoverFile: Invalid id exception, throws at the top level");
                throw iiex;
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.Error("AlbumManager | GetAlbumCoverFile: entry not present exception, throws at the top level");
                throw enpex;
            }
            catch (Exception ex)
            {
                _logger.Error("AlbumManager | GetAlbumCoverFile: A generic error occurred " + ex.Message);
            }
            return data;
        }
    }
}
