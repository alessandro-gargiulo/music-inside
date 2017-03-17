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
        protected readonly string connString;

        public BaseDataAccess(SongDBContext context, IConfiguration conf)
        {
            _db = context;
            connString = conf.GetConnectionString("MusicInsideDatabase");
        }
    }
}
