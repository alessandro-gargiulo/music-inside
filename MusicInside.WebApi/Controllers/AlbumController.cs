using log4net;
using Microsoft.AspNetCore.Mvc;
using MusicInside.Managers.Entities;
using MusicInside.Managers.Exceptions;
using MusicInside.Managers.Implementations;
using MusicInside.WebApi.Contracts;
using MusicInside.WebApi.Shared;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace MusicInside.WebApi.Controllers
{
    [Route(WebConstants.ROUTES.ALBUM_ROUTE)]
    public class AlbumController : Controller
    {
        private AlbumManager _albumManager;
        private ILog _logger;

        public AlbumController(AlbumManager albumManager, ILog logger)
        {
            _albumManager = albumManager;
            _logger = logger;
        }

        [HttpGet(WebConstants.ROUTES.ALBUM_SUB_COVER_ROUTE)]
        public ActionResult GetCoverImage(int id = -1)
        {
            try
            {
                byte[] imageBytes = _albumManager.GetCoverFile(id);
                return base.File(imageBytes, "image/png");
            }
            catch (InvalidIdException iidex)
            {
                _logger.ErrorFormat("{0} | {1}: An error occurred [{2}]", this.GetType().Name, MethodInfo.GetCurrentMethod().Name, iidex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { status = (int)HttpStatusCode.BadRequest, message = iidex.Message });
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("{0} | {1}: An error occurred [{2}]", this.GetType().Name, MethodInfo.GetCurrentMethod().Name, enpex.Message);
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { status = (int)HttpStatusCode.NotFound, message = enpex.Message });
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("{0} | {1}: An error occurred [{2}]", this.GetType().Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { status = (int)HttpStatusCode.InternalServerError, message = "The server can't process your request at this time" });
            }
        }

        [HttpGet(WebConstants.ROUTES.ALBUM_SUB_LIST_ROUTE)]
        public IList<CAlbumListEntry> GetList()
        {
            IList<CAlbumListEntry> result = new List<CAlbumListEntry>();
            try
            {
                List<EAlbum> albums = _albumManager.GetAll();
                foreach(EAlbum album in albums)
                {
                    EArtist artist = _albumManager.GetArtistInfo(album.Id);
                    result.Add(new CAlbumListEntry
                    {
                        Id = album.Id,
                        Name = album.Title,
                        ArtistId = artist.Id,
                        ArtistName = artist.ArtName,
                        NumberSong = _albumManager.GetNumberOfSongs(album.Id)
                    });
                }
            }catch(Exception ex)
            {
                _logger.ErrorFormat("{0} | {1}: An error occurred [{2}]", this.GetType().Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return result;
        }

        [HttpGet(WebConstants.ROUTES.ALBUM_SUB_DETAIL_ROUTE)]
        public CAlbumDetail GetAlbum(int id = -1)
        {
            CAlbumDetail result = new CAlbumDetail();
            try
            {
                EAlbum album = _albumManager.GetAlbumById(id);
                if(album != null)
                {
                    result.Id = album.Id;
                    result.Title = album.Title;
                    result.Artist = album.ArtistName;
                    List<ESong> songs = _albumManager.GetSongsInAlbum(id);
                    foreach(ESong sng in songs)
                    {
                        result.SongList.Add(new SongShortInfo
                        {
                            ID = sng.Id,
                            Title = sng.Title
                        });
                    }
                }
            }
            catch (InvalidIdException iidex)
            {
                _logger.ErrorFormat("{0} | {1}: An error occurred [{2}]", this.GetType().Name, MethodInfo.GetCurrentMethod().Name, iidex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("{0} | {1}: An error occurred [{2}]", this.GetType().Name, MethodInfo.GetCurrentMethod().Name, enpex.Message);
                Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("{0} | {1}: An error occurred [{2}]", this.GetType().Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return result;
        }
    }
}