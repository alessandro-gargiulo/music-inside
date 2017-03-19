﻿using log4net;
using Microsoft.Extensions.Configuration;
using MusicInside.DataAccessInterfaces;
using MusicInside.Exceptions;
using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.DataAccess
{
    public class AlbumDataAccess : BaseDataAccess, IAlbumDataAccess
    {
        public AlbumDataAccess(SongDBContext context, IConfiguration conf, ILog logger) : base(context, conf, logger) { }

        public Album GetAlbumById(int id)
        {
            {
                if (id < 0) throw new InvalidIdException("Invalid album id value. Value must be non-negative");
                Album album = null;
                try
                {
                    album = _db.Albums.Where(x => x.ID == id).FirstOrDefault();
                    if (album == null)
                    {
                        throw new EntryNotPresentException("Can't found an album with chosen id");
                    }
                }
                catch (ArgumentNullException anex)
                {
                    _logger.Error("AlbumDataAccess | GetAlbumById: Cannot execute query with null argument: " + anex.Message);
                }
                return album;
            }
        }

        public byte[] GetCoverFile(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid album id value. Value must be non-negative");
            byte[] arrayByte = null;
            try
            {
                File file = _db.Files.Where(x => x.ID == id).FirstOrDefault();
                if(file == null)
                {
                    throw new EntryNotPresentException("Can't found an album cover with chosen id");
                }
                string path = System.IO.Path.Combine(_fileMusicRoot, file.Path, file.FileName + "." + file.Extension);
                arrayByte = System.IO.File.ReadAllBytes(path);
            }
            catch(System.IO.FileNotFoundException fnfex)
            {
                _logger.Error("AlbumDataAccess | GetCoverFile: Cannot found file at given path: " + fnfex.Message);
            }
            catch(Exception ex)
            {
                _logger.Error("AlbumDataAccess | GetCoverFile: A generic exception occurred " + ex.Message);
            }
            return arrayByte;
        }
    }
}
