﻿using log4net;
using Microsoft.Extensions.Configuration;
using MusicInside.DataAccessInterfaces;
using MusicInside.Exceptions;
using MusicInside.Models;
using System;
using System.Linq;

namespace MusicInside.DataAccess
{
    public class FileDataAccess : BaseDataAccess, IFileDataAccess
    {
        public FileDataAccess(SongDBContext context, IConfiguration conf, ILog logger) : base(context, conf, logger) { }

        public File GetFileById(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid file id value. Value must be non-negative");
            File file = null;
            try
            {
                file = _db.Files.Where(x => x.ID == id).FirstOrDefault();
                if (file == null)
                {
                    throw new EntryNotPresentException("Can't found a file with chosen id");
                }
            }
            catch (ArgumentNullException anex)
            {
                _logger.ErrorFormat("FileDataAccess | GetFileById: Cannot execute query with null argument [{0}]", anex.Message);
            }
            return file;
        }

        public byte[] GetFileBytesById(int id)
        {
            if (id < 0) throw new InvalidIdException("Invalid file id value. Value must be non-negative");
            File file = null;
            byte[] bytes = null;
            try
            {
                file = _db.Files.Where(x => x.SongId == id).FirstOrDefault();
                if(file == null)
                {
                    throw new EntryNotPresentException("Can't found a file with chosen id");
                }
                else
                {
                    string path = System.IO.Path.Combine(_fileMusicRoot, file.Path, file.FileName + "." + file.Extension);
                    bytes = System.IO.File.ReadAllBytes(path);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("FileDataAccess | GetFileBytesById: Generic exception occurred [{0}]", ex.Message);
            }
            return bytes;
        }
    }
}
