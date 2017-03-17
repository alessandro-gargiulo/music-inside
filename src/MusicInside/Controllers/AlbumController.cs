using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicInside.ManagerInterfaces;

namespace MusicInside.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumManager _albumManager;

        public AlbumController(IAlbumManager manager)
        {
            _albumManager = manager;
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
