using Microsoft.Extensions.Configuration;
using MusicInside.DataAccessInterfaces;
using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.DataAccess
{
    public class ArtistDataAccess : BaseDataAccess, IArtistDataAccess
    {
        public ArtistDataAccess(SongDBContext context, IConfiguration conf) : base(context, conf) { }
    }
}
