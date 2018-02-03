using MusicInside.ModelView;
using System.Collections.Generic;

namespace MusicInside.ManagerInterfaces
{
    public interface IAlbumManager
    {
        List<AlbumRowViewModel> GetAllTable();
        AlbumDetailViewModel GetDetailById(int id);
        byte[] GetAlbumCoverFile(int id);
    }
}
