using MusicInside.ModelView;
using System.Collections.Generic;

namespace MusicInside.ManagerInterfaces
{
    public interface ISongManager
    {
        List<SongRowViewModel> GetAllTable();

        List<SongRowViewModel> GetTablePart(string letter);

        SongDetailViewModel GetDetailById(int id);

        byte[] GetFileBytesById(int id);

        void AddPlayToSongId(int id);
    }
}
