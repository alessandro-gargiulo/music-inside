using MusicInside.Managers.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MusicInside.Managers.Interfaces
{
    public interface ISongManager
    {
        /// <summary>
        /// Retrieve all data from database
        /// </summary>
        /// <returns>A list of all songs into the database</returns>
        List<ESong> GetAll();
        /// <summary>
        /// Retrieve all data from database which title starts with he input string
        /// </summary>
        /// <param name="initialString">A letter or a substring</param>
        /// <returns>A list of songs</returns>
        List<ESong> GetStartingWithString(string initialString);
        /// <summary>
        /// Retrieve all data from database which title match the regex
        /// </summary>
        /// <param name="regex">The regex to match for</param>
        /// <returns>A list of songs</returns>
        List<ESong> GetMatchingRegex(Regex regex);
        /// <summary>
        /// Retrieve a single song
        /// </summary>
        /// <param name="id">The identifier of the song</param>
        /// <returns>A sigle song</returns>
        ESong GetSongById(int id);
        /// <summary>
        /// Retrieve the artist information about the song
        /// </summary>
        /// <param name="id">The identifier of the song</param>
        /// <returns>A single artist, the principal artist</returns>
        EArtist GetArtistInfo(int id);
        /// <summary>
        /// Retrieve the album information which contains the song
        /// </summary>
        /// <param name="id">The identifier of the song</param>
        /// <returns>A single album entity</returns>
        EAlbum GetAlbumInfo(int id);
        /// <summary>
        /// Retrieve the media file for the album
        /// </summary>
        /// <param name="id">The identifier of the song</param>
        /// <returns>The byte file represented the song file</returns>
        byte[] GetFileBytes(int id);
        /// <summary>
        /// Increment by one the count of number of play for the selected song
        /// </summary>
        /// <param name="id">The identifier of the song</param>
        /// <param name="when">The datetime when the increment take place</param>
        void AddPlayToSongId(int id,  DateTime when);
        /// <summary>
        /// Retrieve all moments for the song
        /// </summary>
        /// <param name="id">The identifier of the song</param>
        /// <returns>A list of moments</returns>
        List<EMoment> GetMomentsForSong(int id);
        /// <summary>
        /// Retrieve al genres for the song
        /// </summary>
        /// <param name="id">The identifier of the song</param>
        /// <returns>A list of genres</returns>
        List<EGenre> GetGenresForSong(int id);
    }
}
