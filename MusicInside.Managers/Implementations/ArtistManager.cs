using log4net;
using Microsoft.EntityFrameworkCore;
using MusicInside.Managers.Entities;
using MusicInside.Managers.Exceptions;
using MusicInside.Managers.Interfaces;
using MusicInside.Managers.Context;
using MusicInside.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicInside.Managers.Implementations
{
    public class ArtistManager : IArtistManager
    {
        private MusicInsideDbContext _dbContext;
        private ILog _logger;

        public ArtistManager(MusicInsideDbContext context, ILog logger)
        {
            _dbContext = context;
            _logger = logger;
        }

        public List<EArtist> GetAll()
        {
            List<EArtist> eArtistList = new List<EArtist>();
            try
            {
                List<Artist> artists = _dbContext.Artists.ToList();
                foreach (Artist artist in artists)
                {
                    EArtist eArtist = new EArtist();
                    eArtist.CopyFromModel(artist);
                    eArtistList.Add(eArtist);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return eArtistList;
        }

        public EArtist GetArtistById(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            EArtist eArtist = new EArtist();
            try
            {
                Artist artist = _dbContext.Artists.Where(x => x.Id == id).FirstOrDefault();
                if (artist != null)
                {
                    eArtist.CopyFromModel(artist);
                }
                else
                {
                    throw new EntryNotPresentException(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return eArtist;
        }

        public List<ESong> GetFeaturingSongsForArtist(int id, bool isPrincipalArtist)
        {
            if (id < 0) throw new InvalidIdException(id);
            List<ESong> eSongList = new List<ESong>();
            try
            {
                Artist artist = _dbContext.Artists.Where(x => x.Id == id).Include(y => y.Songs).FirstOrDefault();
                if(artist != null)
                {
                    List<Song> songs = _dbContext.Songs.Where(y => artist.Songs.Where(h => h.IsPrincipalArtist == isPrincipalArtist).Select(k => k.Id).Contains(y.Id)).ToList();
                    foreach(Song song in songs)
                    {
                        ESong eSong = new ESong();
                        eSong.CopyFromModel(song);
                        eSongList.Add(eSong);
                    }
                }
                else
                {
                    throw new EntryNotPresentException(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return eSongList;
        }

        public int GetNumberOfSongs(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            int number = 0;
            try
            {
                Artist artist = _dbContext.Artists.Where(x => x.Id == id).Include(y => y.Songs).FirstOrDefault();
                if (artist != null)
                {
                    List<Song> songs = _dbContext.Songs.Where(y => artist.Songs.Select(k => k.Id).Contains(y.Id)).ToList();
                    number = songs.Count();
                }
                else
                {
                    throw new EntryNotPresentException(id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return number;
        }
    }
}
