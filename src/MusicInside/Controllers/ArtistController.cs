using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicInside.ManagerInterfaces;
using log4net;

namespace MusicInside.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistManager _artistManager;
        private readonly ILog _logger;
        public ArtistController(IArtistManager manager, ILog logger)
        {
            _artistManager = manager;
            _logger = logger;
        }
        // GET: /<controller>/
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
