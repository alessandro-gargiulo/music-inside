using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicInside.ManagerInterfaces;
using log4net;

namespace MusicInside.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumManager _albumManager;
        private readonly ILog _logger;

        public AlbumController(IAlbumManager manager, ILog logger)
        {
            _albumManager = manager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id = -1)
        {
            return View();
        }
    }
}
