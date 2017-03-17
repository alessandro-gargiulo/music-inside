using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicInside.Models;
using MusicInside.ManagerInterfaces;
using MusicInside.ModelView;
using log4net;

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
            List<SongRowViewModel> allSong = _songManager.getAllSongs();
            return View(allSong);
        }

        public IActionResult Detail(int id = -1)
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStreamingAudio(int id = -1)
        {
            byte[] stream = System.IO.File.ReadAllBytes("../MusicInside/Data/FlipsydeSomeday.mp3");
            return File(stream, "audio/mp3");
        }
    }
}
