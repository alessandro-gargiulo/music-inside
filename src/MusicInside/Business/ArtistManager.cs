using log4net;
using MusicInside.DataAccessInterfaces;
using MusicInside.ManagerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.Business
{
    public class ArtistManager : IArtistManager
    {
        private readonly IArtistDataAccess _artistDataAccess;
        private readonly ILog _logger;

        public ArtistManager(IArtistDataAccess dataAccess, ILog logger)
        {
            _artistDataAccess = dataAccess;
            _logger = logger;
        }
    }
}
