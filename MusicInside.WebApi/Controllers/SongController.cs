using log4net;
using Microsoft.AspNetCore.Mvc;
using MusicInside.Managers.Entities;
using MusicInside.Managers.Implementations;
using MusicInside.WebApi.Contracts;
using MusicInside.WebApi.Shared;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace MusicInside.WebApi.Controllers
{
    [Route(WebConstants.ROUTES.SONG_ROUTE)]
    public class SongController : Controller
    {
        private SongManager _songManager;
        private ILog _logger;

        public SongController(SongManager songManager, ILog logger)
        {
            _songManager = songManager;
            _logger = logger;
        }

        [HttpGet(WebConstants.ROUTES.SONG_SUB_LIST_ROUTE)]
        public IList<CSongListEntry> GetList()
        {
            List<CSongListEntry> result = new List<CSongListEntry>();
            try
            {
                List<ESong> songs = _songManager.GetAll();
                foreach (ESong song in songs)
                {
                    List<EGenre> genres = _songManager.GetGenresForSong(song.Id);
                    EAlbum eAlbum = _songManager.GetAlbumInfo(song.Id);
                    EArtist eArtist = _songManager.GetArtistInfo(song.Id);
                    result.Add(new CSongListEntry
                    {
                        Id = song.Id,
                        Title = song.Title,
                        ArtistName = string.Concat(eArtist.Name, " ", eArtist.Surname),
                        AlbumName = eAlbum.Title,
                        Genre = string.Join(" ,", genres),
                        Year = song.Year,
                        ArtistId = eArtist.Id,
                        AlbumId = eAlbum.Id
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("{0} | {1}: An error occurred [{2}]", this.GetType().Name, MethodInfo.GetCurrentMethod().Name, ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            return result;
        }

        [HttpGet(WebConstants.ROUTES.SONG_SUB_DETAIL_ROUTE)]
        public CSongDetail GetSong(int id = -1)
        {
            CSongDetail result = new CSongDetail();

            return result;
        }
    }
}