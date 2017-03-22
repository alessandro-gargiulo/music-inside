using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicInside.Models;
using MusicInside.ManagerInterfaces;
using MusicInside.ModelView;
using log4net;
using MusicInside.Exceptions;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Net.Http;
using System.Net;

namespace MusicInside.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongManager _songManager;
        private readonly ILog _logger;
        public SongController(ISongManager manager, ILog logger) {
            _songManager = manager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<SongRowViewModel> allSong = _songManager.GetAllSongs();
            return View(allSong);
        }

        public IActionResult Detail(int id = -1)
        {
            try
            {
                SongDetailViewModel songDetail = _songManager.GetDetailOfSong(id);
                return View(songDetail);
            }
            catch (InvalidIdException iiex)
            {
                _logger.Error("SongController | Detail: " + iiex.Message);
                return null; // Redirect to error screen
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.Error("SongController | Detail: " + enpex.Message);
                return null; // Redirect to error screen
            }catch(Exception ex)
            {
                _logger.Error("SongController | Detail: A generic error occurred " + ex.Message);
                return null;
            }
        }

        [HttpGet]
        public ActionResult GetStreamingAudio(int id = -1)
        {
            byte[] song = System.IO.File.ReadAllBytes("../MusicInside/Data/FlipsydeSomeday.mp3");
            long fSize = song.Length;
            long startByte = 0;
            long endByte = 0;
            int statusCode = 200;

            var rangeRequest = Request.Headers["Range"].ToString();

            if(rangeRequest != "")
            {
                string[] range = Request.Headers["Range"].ToString().Split(new char[] { '=', '-' });
                startByte = Convert.ToInt64(range[1]);
                if(range.Length > 2 && range[2] != "")
                {
                    endByte = Convert.ToInt64(range[2]);
                }
                if (startByte != 0 || endByte != fSize - 1 || range.Length > 2 && range[2] == "")
                {
                    statusCode = 206;
                }
            }

            long desSize = endByte == 0 ? fSize - startByte : endByte - startByte + 1;

            Response.StatusCode = statusCode;
            Response.ContentType = "audio/mp3";
            Response.Headers.Add("Content-Accept", Response.ContentType);
            Response.Headers.Add("Content-Length", desSize.ToString());
            Response.Headers.Add("Content-Range", string.Format("bytes {0}-{1}/{2}", startByte, endByte, fSize));
            Response.Headers.Add("Accept-Ranges", "bytes");
            Response.Headers.Remove("Cache-Control");

            var stream = new MemoryStream(song, (int)startByte, (int)desSize);
            return File(stream, Response.ContentType);
        }
    }
}
