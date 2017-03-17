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

        public AlbumManager(IAlbumDataAccess dataAccess)
        {
            _albumDataAccess = dataAccess;
        }
    }
}
