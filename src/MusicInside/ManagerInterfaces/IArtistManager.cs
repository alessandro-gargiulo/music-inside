using MusicInside.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.ManagerInterfaces
{
    public interface IArtistManager
    {
        ArtistDetailViewModel GetDetailById(int id);
    }
}
