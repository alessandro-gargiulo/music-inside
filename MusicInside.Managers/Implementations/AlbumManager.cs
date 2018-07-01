using log4net;
using Microsoft.EntityFrameworkCore;
using MusicInside.Managers.Context;
using MusicInside.Managers.Entities;
using MusicInside.Managers.Exceptions;
using MusicInside.Managers.Interfaces;
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

        public EArtist GetArtistInfo(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            EArtist eArtist = new EArtist();
            try
            {
                // Retrieve the first song of the album
                Song firstSong = _dbContext.Albums.Where(x => x.Id == id).FirstOrDefault().Songs.FirstOrDefault();
                if (firstSong != null)
                {
                    int artistId = firstSong.Artists.FirstOrDefault(x => x.IsPrincipalArtist == true).ArtistId;
                    Artist artist = _dbContext.Artists.Where(x => x.Id == artistId).FirstOrDefault();
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
                    eAlbum.NumberSong = GetNumberOfSongs(album.Id);
                    EArtist artist = GetArtistInfo(album.Id);
                    eAlbum.ArtistId = artist.Id;
                    eAlbum.ArtistName = string.Concat(artist.Name, " ", artist.Surname);
                    eAlbumList.Add(eAlbum);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return eAlbumList;
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
                    foreach (Song song in album.Songs)
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
    }
}
