﻿using log4net;
using Microsoft.Extensions.Configuration;
using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.DataAccess
{

    public class BaseDataAccess
    {
        protected SongDBContext _db;
        protected readonly string _connString;
        private readonly ILog _logger;

        public BaseDataAccess(SongDBContext context, IConfiguration conf, ILog logger)
        {
            _db = context;
            _connString = conf.GetConnectionString("MusicInsideDatabase");
            _logger = logger;
        }
    }
}
