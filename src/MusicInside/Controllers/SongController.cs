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
            byte[] stream = System.IO.File.ReadAllBytes("../MusicInside/Data/FlipsydeSomeday.mp3");
            return File(stream, "audio/mp3");
        }
    }
}
