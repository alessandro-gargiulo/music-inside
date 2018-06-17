using log4net;
using Microsoft.EntityFrameworkCore;
using MusicInside.Managers.Entities;
using MusicInside.Managers.Exceptions;
using MusicInside.Managers.Interfaces;
using MusicInside.Managers.Context;
using MusicInside.Models.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MusicInside.Managers.Implementations
{
    public class AlbumManager : IAlbumManager
    {
        private MusicInsideDbContext _dbContext;
        private ILog _logger;
        private string _fileMusicRoot;

        public AlbumManager(MusicInsideDbContext context, ILog logger, string fileMusicRoot)
        {
            _dbContext = context;
            _logger = logger;
            _fileMusicRoot = fileMusicRoot;
        }

        public EAlbum GetAlbumById(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            EAlbum eAlbum = new EAlbum();
            try
            {
                Album album = _dbContext.Albums.Where(x => x.Id == id).FirstOrDefault();
                if (album != null)
                {
                    eAlbum.CopyFromModel(album);
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
            return eAlbum;
        }

        public List<EAlbum> GetAll()
        {
            List<EAlbum> eAlbumList = new List<EAlbum>();
            try
            {
                List<Album> albums = _dbContext.Albums.ToList();
                foreach(Album album in albums)
                {
                    EAlbum eAlbum = new EAlbum();
                    eAlbum.CopyFromModel(album);
                    eAlbumList.Add(eAlbum);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return eAlbumList;
        }

        public byte[] GetCoverFile(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            byte[] arrayByte = null;
            try
            {
                CoverFile file = _dbContext.Covers.Where(x => x.Id == id).FirstOrDefault();
                if (file == null)
                {
                    throw new EntryNotPresentException(id);
                }
                string path = Path.Combine(_fileMusicRoot, file.Path, file.FileName + "." + file.Extension);
                arrayByte = File.ReadAllBytes(path);
            }
            catch (Exception)
            {
                throw;
            }
            return arrayByte;
        }

        public int GetNumberOfSongs(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            int number = 0;
            try
            {
                Album album = _dbContext.Albums.Where(x => x.Id == id).Include(y => y.Songs).FirstOrDefault();
                if (album != null)
                {
                    number = album.Songs.Count();
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

        public List<ESong> GetSongsInAlbum(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            List<ESong> songs = new List<ESong>();
            try
            {
                Album album = _dbContext.Albums.Where(x => x.Id == id).Include(y => y.Songs).FirstOrDefault();
                if (album != null)
                {
                    foreach(Song song in album.Songs)
                    {
                        ESong eSong = new ESong();
                        eSong.CopyFromModel(song);
                        songs.Add(eSong);
                    }
                }
                else
                {
                    throw new EntryNotPresentException(id);
                }
                return songs;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
