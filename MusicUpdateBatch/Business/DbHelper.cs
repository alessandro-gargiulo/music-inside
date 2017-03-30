using MusicInside.Models;
using MusicUpdateBatch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TagLib;

namespace MusicUpdateBatch.Business
{
    public class DbHelper : IDisposable, IDbHelper
    {
        private readonly SongDBContext _context;
        private readonly log4net.ILog _logger;

        public DbHelper(log4net.ILog log, SongDBContext context)
        {
            _context = context;
            _logger = log;
        }

        public int InsertSong(Tag songTag)
        {
            try
            {
                Song newSong = new Song();
                newSong.Title = songTag.Title;
                newSong.TrackNo = (int)songTag.Track;
                newSong.Year = (int)songTag.Year;
                _context.Songs.Add(newSong);
                _context.SaveChanges();
                return newSong.ID;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("DbHelper | InsertSong: Can't insert song with title <{0}> due to exception [{1}]", songTag.Title, ex.Message);
                return -1;
            }
        }

        public int TryToInsertArtist(Tag songTag)
        {
            try
            {
                // Retrieve artist info
                string principalArtist = songTag.FirstAlbumArtist;
                // Search in database for the artist
                Artist dbArtist = _context.Artists.Where(x => x.ArtName == principalArtist).FirstOrDefault();
                if(dbArtist == null)
                {
                    // If does not exist, insert a new one
                    dbArtist.ArtName = principalArtist;
                    _context.Artists.Add(dbArtist);
                    _context.SaveChanges();
                    // TODO: retrieve true name and birthday
                }
                else
                {
                    // If exist, write log
                    _logger.WarnFormat("DbHelper | TryToInsertArtist: an artist with artName={0} was already found with ID={1}", dbArtist.ArtName, dbArtist.ID);
                }
                return dbArtist.ID;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("DbHelper | TryToInsertArtist: Can't insert/find artist with name <{0}> due to exception [{1}]", songTag.FirstAlbumArtist, ex.Message);
                return -1;
            }
        }

        public int TryToInsertAlbumForArtist(TagLib.Tag songTag, int artistId)
        {   //check artistId before...........
            try
            {
                // Retrieve album info
                string albumTitle = songTag.Album;
                // Search in database for an album with same artist and title
                Album dbAlbum = _context.Albums.Where(x => x.Title == albumTitle)
                                                .Where(y => y.ArtistId == artistId)
                                                .FirstOrDefault();
                if(dbAlbum == null)
                {
                    // If does not exist, insert a new one
                    dbAlbum.ArtistId = artistId;
                    dbAlbum.Title = albumTitle;
                    _context.Albums.Add(dbAlbum);
                    _context.SaveChanges();
                }
                else
                {
                    // If exist, write log
                    _logger.WarnFormat("DbHelper | TryToInsertAlbum: an album with title={0} was already found with ID={1}", dbAlbum.Title, dbAlbum.ID);
                }
                return dbAlbum.ID;
            }
            catch(Exception ex)
            {
                _logger.ErrorFormat("DbHelper | TryToInsertAlbum: Can't insert/find album with title <{0}> for artistId={1} due to exception [{2}]", songTag.Album, artistId, ex.Message);
                return -1;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
