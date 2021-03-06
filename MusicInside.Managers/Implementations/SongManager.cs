﻿using log4net;
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
using System.Text.RegularExpressions;

namespace MusicInside.Managers.Implementations
{
    public class SongManager : ISongManager
    {
        #region Private Members
        private MusicInsideDbContext _dbContext;
        private ILog _logger;
        private string _fileMusicRoot;
        #endregion

        #region Constructors
        public SongManager(MusicInsideDbContext context, ILog logger, string fileMusicRoot)
        {
            _dbContext = context;
            _logger = logger;
            _fileMusicRoot = fileMusicRoot;
        }
        #endregion

        #region Manager Implementation
        public ESong GetSongById(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            ESong eSong = new ESong();
            try
            {
                Song song = _dbContext.Songs.Where(x => x.Id == id).FirstOrDefault();
                if (song != null)
                {
                    eSong.CopyFromModel(song);
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
            return eSong;
        }

        public EArtist GetArtistInfo(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            EArtist eArtist = new EArtist();
            try
            {
                // Retrieve the first song of the album
                Song song = _dbContext.Songs.Where(x => x.Id == id).FirstOrDefault();
                if (song != null)
                {
                    int artistId = song.Artists.FirstOrDefault(x => x.IsPrincipalArtist == true).ArtistId;
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

        public EAlbum GetAlbumInfo(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            EAlbum eAlbum = new EAlbum();
            try
            {
                Album album = _dbContext.Songs.Where(x => x.Id == id).FirstOrDefault().Album;
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

        public List<ESong> GetAll()
        {
            List<ESong> eSongList = new List<ESong>();
            try
            {
                List<Song> songs = _dbContext.Songs.ToList();
                foreach (Song song in songs)
                {
                    ESong eSong = new ESong();
                    eSong.CopyFromModel(song);
                    eSongList.Add(eSong);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return eSongList;
        }

        public List<ESong> GetStartingWithString(string initialString)
        {
            List<ESong> eSongList = new List<ESong>();
            try
            {
                List<Song> songs = _dbContext.Songs.Where(x => x.Title.StartsWith(initialString)).ToList();
                foreach (Song song in songs)
                {
                    ESong eSong = new ESong();
                    eSong.CopyFromModel(song);
                    eSongList.Add(eSong);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return eSongList;
        }

        public List<ESong> GetMatchingRegex(Regex regex)
        {
            List<ESong> eSongList = new List<ESong>();
            try
            {
                List<Song> songs = _dbContext.Songs.Where(x => regex.IsMatch(x.Title)).ToList();
                foreach (Song song in songs)
                {
                    ESong eSong = new ESong();
                    eSong.CopyFromModel(song);
                    eSongList.Add(eSong);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return eSongList;
        }

        public List<EGenre> GetGenresForSong(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            List<EGenre> eGenreList = new List<EGenre>();
            try
            {
                Song song = _dbContext.Songs.Where(x => x.Id == id).Include(y => y.Genres).FirstOrDefault();
                if (song != null)
                {
                    List<Genre> genres = _dbContext.Genres.Where(x => song.Genres.Select(y => y.GenreId).Contains(x.Id)).ToList();
                    foreach(Genre genre in genres)
                    {
                        EGenre eGenre = new EGenre();
                        eGenre.CopyFromModel(genre);
                        eGenreList.Add(eGenre);
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
            return eGenreList;
        }

        public List<EMoment> GetMomentsForSong(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            List<EMoment> eMomentList = new List<EMoment>();
            try
            {
                Song song = _dbContext.Songs.Where(x => x.Id == id).Include(y => y.Moments).FirstOrDefault();
                if (song != null)
                {
                    List<Moment> moments = _dbContext.Moments.Where(x => song.Moments.Select(y => y.MomentId).Contains(x.Id)).ToList();
                    foreach (Moment moment in moments)
                    {
                        EMoment eMoment = new EMoment();
                        eMoment.CopyFromModel(moment);
                        eMomentList.Add(eMoment);
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
            return eMomentList;
        }

        public byte[] GetFileBytes(int id)
        {
            if (id < 0) throw new InvalidIdException(id);
            byte[] arrayByte = null;
            try
            {
                MediaFile file = _dbContext.Medias.Where(x => x.Id == id).FirstOrDefault();
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

        public void AddPlayToSongId(int id, DateTime when)
        {
            if (id < 0) throw new InvalidIdException(id);
            ESong eSong = new ESong();
            try
            {
                Song song = _dbContext.Songs.Where(x => x.Id == id).Include(y => y.Statistic).FirstOrDefault();
                if (song != null)
                {
                    if (song.Statistic != null)
                    {
                        // Exist, update
                        song.Statistic.NumPlay++;
                        song.Statistic.LastPlay = when;
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        // Does not exist, create a new one
                        Statistic statistic = new Statistic();
                        statistic.NumPlay = 1;
                        statistic.LastPlay = when;
                        song.Statistic = statistic;
                        _dbContext.SaveChanges();
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
        }
        #endregion
    }
}
