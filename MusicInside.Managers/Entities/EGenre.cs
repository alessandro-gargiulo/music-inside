using MusicInside.Models.Models;

namespace MusicInside.Managers.Entities
{
    public class EGenre
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public void CopyFromModel(Genre genre)
        {
            this.Id = genre.Id;
            this.Description = genre.Description;
        }
    }
}
