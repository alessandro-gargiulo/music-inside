namespace MusicInside.WebApi.Shared
{
    public class WebConstants
    {
        public struct ROUTES
        {
            #region Album Controller Routes
            public const string ALBUM_ROUTE = "api/albums";
            public const string ALBUM_SUB_LIST_ROUTE = "list";
            public const string ALBUM_SUB_DETAIL_ROUTE = "list/{id}";
            public const string ALBUM_SUB_COVER_ROUTE = "cover/{id}";
            #endregion
            public const string ARTIST_ROUTE = "api/artist";
            public const string SONG_ROUTE = "api/songs";
        }
    }
}
