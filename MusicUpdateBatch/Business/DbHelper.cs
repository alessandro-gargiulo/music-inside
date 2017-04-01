using Microsoft.Extensions.Configuration;
using MusicInside.Models;
using MusicUpdateBatch.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TagLib;

namespace MusicUpdateBatch.Business
{
    public class DbHelper : IDisposable, IDbHelper
    {
        private readonly SongDBContext _context;
        private readonly log4net.ILog _logger;
        private readonly string _fileMusicRoot;
        private readonly string _coverPath;

        public DbHelper(log4net.ILog log, SongDBContext context, IConfiguration conf)
        {
            _context = context;
            _logger = log;
            // Retrieve root directory from configuration
            _fileMusicRoot = conf.GetSection("MusicFiles").GetValue<string>("RootDirectory");
            // Retrieve cover root directory from configuration
            _coverPath = conf.GetSection("MusicFiles").GetValue<string>("CoverDirectory");
        }

        public int InsertOrUpdateSong(Tag songTag)
        {
            try
            {
                int existingSongId = CheckSongExistence(songTag);
                // Check if a song already exist in database...
                if (existingSongId == 0)
                {
                    _logger.InfoFormat("DbHelper | InsertOrUpdateSong: Song with title <{0}> will be created", songTag.Title);
                    // If song doesn't exist, create a new one
                    Song newSong = new Song()
                    {
                        Title = songTag.Title,
                        TrackNo = (int)songTag.Track,
                        Year = (int)songTag.Year
                    };
                    _context.Songs.Add(newSong);
                    _context.SaveChanges();
                    existingSongId = newSong.ID;
                    _logger.InfoFormat("DbHelper | InsertOrUpdateSong: Song with title <{0}> was correctly insert with id={1}", songTag.Title, existingSongId);
                }
                else
                {
                    _logger.InfoFormat("DbHelper | InsertOrUpdateSong: Song with title <{0}> will be updated", songTag.Title);
                    // If song exist, update its informations
                    Song existingSong = _context.Songs.Where(x => x.ID == existingSongId).FirstOrDefault();
                    if (existingSong == null) throw new Exception("Something goes wrong when retrieving declared existing song from database");
                    existingSong.Title = songTag.Title;
                    existingSong.TrackNo = (int)songTag.Track;
                    existingSong.Year = (int)songTag.Year;
                    _context.Songs.Update(existingSong);
                    _context.SaveChanges();
                    _logger.InfoFormat("DbHelper | InsertOrUpdateSong: Song with id={0} was correctly updated", existingSongId);
                }
                return existingSongId;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("DbHelper | InsertOrUpdateSong: Can't insert song with title <{0}> due to exception [{1}]", songTag.Title, ex.Message);
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
                    dbArtist = new Artist()
                    {
                        ArtName = principalArtist
                    };
                    _context.Artists.Add(dbArtist);
                    _context.SaveChanges();
                    _logger.InfoFormat("DbHelper | TryToInsertArtist: Artist with name <{0}> was correctly insert with id={1}", songTag.FirstAlbumArtist, dbArtist.ID);
                    // TODO: retrieve birthday
                    // http://musicbrainz.org/ws/2/artist/?query=artist:tiziano%20ferro
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

        public int TryToInsertAlbumForArtist(Tag songTag, int artistId)
        {
            try
            {
                // Check for valid id
                if (artistId < 1) throw new Exception("Invalid artistId");
                // Retrieve album info
                string albumTitle = songTag.Album;
                // Search in database for an album with same artist and title
                Album dbAlbum = _context.Albums.Where(x => x.Title == albumTitle)
                                                .Where(y => y.ArtistId == artistId)
                                                .FirstOrDefault();
                if (dbAlbum == null)
                {
                    // If does not exist, insert a new one
                    dbAlbum = new Album()
                    {
                        ArtistId = artistId,
                        Title = albumTitle
                    };
                    _context.Albums.Add(dbAlbum);
                    _context.SaveChanges();
                    _logger.InfoFormat("DbHelper | TryToInsertAlbumForArtist: Album with title <{0}> was correctly insert with id={1}", songTag.Album, dbAlbum.ID);
                }
                else
                {
                    // If exist, write log
                    _logger.WarnFormat("DbHelper | TryToInsertAlbumForArtist: an album with title <{0}> was already found with ID={1}", dbAlbum.Title, dbAlbum.ID);
                }
                return dbAlbum.ID;
            }
            catch(Exception ex)
            {
                _logger.ErrorFormat("DbHelper | TryToInsertAlbum: Can't insert/find album with title <{0}> for artistId={1} due to exception [{2}]", songTag.Album, artistId, ex.Message);
                return -1;
            }
        }

        public void UpdateSongAlbum(int songId, int albumId)
        {
            try
            {
                if (songId < 1 || albumId < 1) throw new Exception("Invalid songId or albumId");
                Song song = _context.Songs.Where(x => x.ID == songId).FirstOrDefault();
                if(song != null)
                {
                    song.AlbumId = albumId;
                    _context.Songs.Update(song);
                    _context.SaveChanges();
                    _logger.InfoFormat("DbHelper | UpdateSongAlbum: Song with id={0} was correctly updated with albumId={1}", songId, albumId);
                }
                else
                {
                    throw new Exception("Something goes wrong on Song's albumId update");
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("DbHelper | UpdateSongAlbum: Can't update song with id <{0}> for albumId={1} due to exception [{2}]", songId, albumId, ex.Message);
            }
        }

        public void TryToInsertGenre(Tag songTag, int songId)
        {
            try
            {
                // Retrieve genre info
                //string tagGenre = songTag.FirstGenre;
                string[] genres = songTag.Genres;
                foreach(string tagGenre in genres)
                {
                    // Search in database for the genre
                    Genre dbGenre = _context.Genres.Where(x => x.Description == tagGenre).FirstOrDefault();
                    if (dbGenre == null)
                    {
                        // If does not exist, insert a new one
                        dbGenre = new Genre()
                        {
                            Description = tagGenre
                        };
                        _context.Genres.Add(dbGenre);
                        _context.SaveChanges();
                        _logger.InfoFormat("DbHelper | TryToInsertGenre: Created new genre with id={0} and description={1}", dbGenre.ID, dbGenre.Description);
                    }
                    // Check if relation already exist
                    SongGenre songGenre = _context.SongGenres
                        .Where(x => x.GenreId == dbGenre.ID)
                        .Where(y => y.SongId == songId)
                        .FirstOrDefault();
                    if(songGenre == null)
                    {
                        // Create a relation with genre and song
                        SongGenre insertedGenreRelation = new SongGenre()
                        {
                            GenreId = dbGenre.ID,
                            SongId = songId
                        };
                        _context.Add(insertedGenreRelation);
                        _context.SaveChanges();
                        _logger.InfoFormat("DbHelper | TryToInsertGenre: Created new relation between genre with id={0} and song with id={1}", insertedGenreRelation.GenreId, insertedGenreRelation.SongId);
                    }
                    else
                    {
                        _logger.WarnFormat("DbHelper | TryToInsertGenre: A relation between genreId={0} and songId={1} already exist", songGenre.GenreId, songGenre.SongId);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.ErrorFormat("DbHelper | TryToInsertGenre: Can't insert/find song genre with description <{0}> associated to songId={1} due to exception [{2}]", songTag.FirstGenre, songId, ex.Message);
            }
        }

        public void InsertPhysicalFile(string folder, string fileName, int songId)
        {
            try
            {
                // Retrieving extension without dot
                int dotPos = fileName.LastIndexOf(".") + 1;
                string extension = fileName.Substring(dotPos, fileName.Length - dotPos);
                MusicInside.Models.File existingFile = _context.Files
                    .Where(x => x.SongId == songId)
                    .FirstOrDefault();
                if(existingFile == null)
                {
                    // Create new entry
                    MusicInside.Models.File file = new MusicInside.Models.File
                    {
                        Path = folder,
                        FileName = Path.GetFileNameWithoutExtension(fileName),
                        Extension = extension,
                        SongId = songId
                    };
                    // Add entry and save changes
                    _context.Files.Add(file);
                    _context.SaveChanges();
                    _logger.InfoFormat("DbHelper | InsertPhysicalFile: The file {0}/{1} with songId={2} was correctly inserted in database", folder, fileName, songId);
                }
                else
                {
                    _logger.InfoFormat("DbHelper | InsertPhysicalFile: The file {0}/{1} with songId={2} was already found in database", folder, fileName, songId);
                }

            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("DbHelper | InsertPhysicalFile: Can't insert physical file /{0}/{1} associated to songId={2} due to exception [{3}]", folder, fileName, songId, ex.Message);
            }
        }

        public int KeepCoverFile(Tag songTag, int albumId)
        {
            try
            {
                if (albumId < 1) throw new Exception("Invalid albumId");
                MusicInside.Models.File existingCoverFile = _context.Files.Where(x => x.AlbumId == albumId).FirstOrDefault();
                if(existingCoverFile == null)
                {
                    // Retrieve image and store in an object
                    if (songTag.Pictures[0] != null)
                    {
                        // Insert a new cover file entry
                        existingCoverFile = new MusicInside.Models.File
                        {
                            AlbumId = albumId,
                            Extension = "png",
                            Path = _coverPath,
                            FileName = "NULL"
                        };
                        _context.Files.Add(existingCoverFile);
                        _context.SaveChanges();
                        _logger.InfoFormat("DbHelper | KeepCoverFile: File for albumId={0} was correctly inserted using id={1}", albumId, existingCoverFile.ID);
                        // Compose new file name
                        string newCoverFileName = existingCoverFile.ID.ToString() + "_" + albumId.ToString();
                        // Check if file doesn not exist
                        if(!System.IO.File.Exists(Path.Combine(_fileMusicRoot, _coverPath, newCoverFileName)))
                        {
                            // File does not exist on file system
                            MemoryStream ms = new MemoryStream(songTag.Pictures[0].Data.Data);
                            using (FileStream fs = new FileStream(Path.Combine(_fileMusicRoot, _coverPath, newCoverFileName) + ".png", FileMode.Create, FileAccess.Write))
                            {
                                byte[] bytes = new byte[ms.Length];
                                ms.Read(bytes, 0, (int)ms.Length);
                                fs.Write(bytes, 0, bytes.Length);
                                ms.Dispose();
                            }
                            _logger.InfoFormat("DbHelper | KeepCoverFile: Created new file on file system under directory /{0}/{1} using name {2}.png", _fileMusicRoot, _coverPath, newCoverFileName);
                            existingCoverFile.FileName = newCoverFileName;
                            _context.Files.Update(existingCoverFile);
                            _context.SaveChanges();
                            _logger.InfoFormat("DbHelper | KeepCoverFile: Field 'fileName' of fileId={0} was correctly updated using value={1}", existingCoverFile.ID, newCoverFileName);
                        }
                    }
                    else
                    {
                        _logger.WarnFormat("DbHelper | KeepCoverFile: Song with title <{0}> and albumId={1} does not have a picture tag", songTag.Title, albumId);
                    }
                }
                else
                {
                    _logger.WarnFormat("DbHelper | KeepCoverFile: Cover of file with albumId={0} does already exist", albumId);
                }
                return existingCoverFile.ID;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("DbHelper | RetrieveCoverFile: Can't retrieve image bytes for song titled {0} of artist {1} due to exception [{2}]", songTag.Title, songTag.FirstAlbumArtist, ex.Message);
                return -1;
            }
        }

        public void UpdateAlbumCoverFileId(int albumId, int fileId)
        {
            try
            {
                if (albumId < 1 || fileId < 1) throw new Exception("Invalid albumId or fileId");
                Album existingAlbum = _context.Albums.Where(x => x.ID == albumId).FirstOrDefault();
                if(existingAlbum != null)
                {
                    existingAlbum.FileId = fileId;
                    _context.Albums.Update(existingAlbum);
                    _context.SaveChanges();
                    _logger.InfoFormat("DbHelper | UpdateAlbumCoverFileId: Album with id={0} was correctly updated with fileId={1}", albumId, fileId);
                }
                else
                {
                    _logger.WarnFormat("DbHelper | UpdateAlbumCoverFileId: An existing album with id={0} was not found. Album was not updated with fileId={1}", albumId, fileId);
                }
            }catch(Exception ex)
            {
                _logger.ErrorFormat("DbHelper | UpdateAlbumCoverFileId: Can't update album cover fileId for albumId={0} due to exception [{1}]", albumId, ex.Message);

            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        // Private Service Methods
        private int CheckSongExistence(Tag songTag)
        {
            _logger.DebugFormat("DbHelper | CheckSongExistence: Attempt to find a duplicate of song <{0}> performed by {1}", songTag.Title, songTag.FirstAlbumArtist);
            int control = 0;
            // Retrieve all possible Song contained into database with same track number
            List<Song> exSong = _context.Songs
                .Where(x => x.Title == songTag.Title)
                .Where(y => y.TrackNo == songTag.Track)
                .ToList();
            // For each one of these...
            foreach(Song sng in exSong)
            {
                // Retrieve album informations
                int albumId = sng.AlbumId.GetValueOrDefault();
                Album exAlbum = _context.Albums.Where(x => x.ID == albumId).FirstOrDefault();
                if(exAlbum != null && exAlbum.Title == songTag.Album)
                {
                    // Exist a song with same title, track number and album. Attempt to evaluate artist.
                    int artistId = exAlbum.ArtistId;
                    // Retrieve artist informations
                    Artist artist = _context.Artists.Where(x => x.ID == artistId).FirstOrDefault();
                    if(artist.ArtName == songTag.FirstAlbumArtist)
                    {
                        // Find a duplicate
                        control = sng.ID;
                        break;
                    }
                }
            }
            return control;
        }
    }
}
