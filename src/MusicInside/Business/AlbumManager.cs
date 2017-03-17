using log4net;
using MusicInside.DataAccessInterfaces;
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
    }
}
