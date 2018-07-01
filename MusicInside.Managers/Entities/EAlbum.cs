using MusicInside.Models.Models;

namespace MusicInside.Managers.Entities
{
    public class EAlbum
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public void CopyFromModel(Album album)
        {
            this.Id = album.Id;
            this.Title = album.Title;
        }
    }
}
