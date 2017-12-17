using MusicInside.ModelView;
using System.Collections.Generic;

namespace MusicInside.ManagerInterfaces
{
    public interface IArtistManager
    {
        ArtistDetailViewModel GetDetailById(int id);

        List<ArtistRowViewModel> GetAllTable();
    }
}
