using log4net;
using MusicInside.Managers.Entities;
using MusicInside.Managers.Interfaces;
using MusicInside.Models.Context;
using System.Collections.Generic;

namespace MusicInside.Managers.Implementations
{
    public class ArtistManager : IArtistManager
    {
        private MusicInsideDbContext _dbContext;
        private ILog _logger;

        public List<EArtist> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public EArtist GetArtistById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<ESong> GetFeaturingSongsForArtist(int id)
        {
            throw new System.NotImplementedException();
        }

        public int GetNumberOfSongs(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<ESong> GetSongsForArtist(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
