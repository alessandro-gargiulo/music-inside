namespace MusicInside.WebApi.Contracts
{
    public class CSongListEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ArtistName { get; set; }
        public string AlbumName { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public int ArtistId { get; set; }
        public int AlbumId { get; set; }
    }
}
