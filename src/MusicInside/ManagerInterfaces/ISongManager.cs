using MusicInside.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
