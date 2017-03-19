using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.ManagerInterfaces
{
    public interface IAlbumManager
    {
        byte[] GetAlbumCoverFile(int id);
    }
}
