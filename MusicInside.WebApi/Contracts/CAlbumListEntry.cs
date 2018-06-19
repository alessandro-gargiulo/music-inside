namespace MusicInside.WebApi.Contracts
{
    public class CAlbumListEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberSong { get; set; }
        public string ArtistName { get; set; }
        public int ArtistId { get; set; }
    }
}
