using MusicInside.ModelView;
using System.Collections.Generic;

namespace MusicInside.ManagerInterfaces
{
    public interface IAlbumManager
    {
        AlbumDetailViewModel GetDetailById(int id);
        List<AlbumRowViewModel> GetAllTable();
        byte[] GetAlbumCoverFile(int id);
    }
}
