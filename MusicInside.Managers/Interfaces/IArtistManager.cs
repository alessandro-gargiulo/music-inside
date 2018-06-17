using MusicInside.Managers.Entities;
using System.Collections.Generic;

namespace MusicInside.Managers.Interfaces
{
    public interface IArtistManager
    {
        /// <summary>
        /// Retrieve all data from database
        /// </summary>
        /// <returns>A list of all artist into the database</returns>
        List<EArtist> GetAll();
        /// <summary>
        /// Retrieve a single artist from database
        /// </summary>
        /// <param name="id">The identifier of the artist</param>
        /// <returns>A single artist</returns>
        EArtist GetArtistById(int id);
        /// <summary>
        /// Retrieve all the song performed in featuring by the artist
        /// </summary>
        /// <param name="id">The identifier of the artist</param>
        /// <param name="isPrincipalArtist">Set true if you want the song which the performer is principal artist, false otherwise</param>
        /// <returns>A list of songs performed in featuring by the artist</returns>
        List<ESong> GetFeaturingSongsForArtist(int id, bool isPrincipalArtist);
        /// <summary>
        /// Calculate how many song are performed by the artist
        /// </summary>
        /// <param name="id">The identifier of the artist</param>
        /// <returns>Number of song contained into the album</returns>
        int GetNumberOfSongs(int id);
    }
}
