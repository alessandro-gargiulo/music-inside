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

        public ArtistManager(IArtistDataAccess dataAccess)
        {
            _artistDataAccess = dataAccess;
        }
    }
}
