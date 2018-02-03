using MusicInside.ModelView;
using System.Collections.Generic;

namespace MusicInside.ManagerInterfaces
{
    public interface IArtistManager
    {
        List<ArtistRowViewModel> GetAllTable();

        ArtistDetailViewModel GetDetailById(int id);
    }
}
