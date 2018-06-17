using MusicInside.Models.Models;

namespace MusicInside.Managers.Entities
{
    public class EArtist
    {
        public int Id { get; set; }
        public string ArtName { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int BirthYear { get; set; }
        public bool IsBand { get; set; }

        public void CopyFromModel(Artist artist)
        {
            this.Id = artist.Id;
            this.ArtName = artist.ArtName;
            this.Name = artist.Name;
            this.Surname = artist.Surname;
            this.BirthYear = artist.BirthYear;
            this.IsBand = artist.IsBand;
        }
    }
}
