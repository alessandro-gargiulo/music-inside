using MusicInside.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.DataAccessInterfaces
{
    public interface ISongDataAccess
    {
        List<SongRowViewModel> getAllSong();
    }
}
