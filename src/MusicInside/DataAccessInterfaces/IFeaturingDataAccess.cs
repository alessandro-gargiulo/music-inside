﻿using System.Collections.Generic;

namespace MusicInside.DataAccessInterfaces
{
    public interface IFeaturingDataAccess
    {
        List<int> GetOnlyFeatsArtistId(int songId);

        int GetPrincipalArtistId(int songId);

        List<int> GetBothPrincipalAndFeatsArtistId(int songId);

        List<int> GetSongFeaturingOfArtistId(int artistId);

        List<int> GetBothSongAndFeaturingOfArtistId(int artistId);
    }
}
