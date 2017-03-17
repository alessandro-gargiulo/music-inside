using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicInside.ManagerInterfaces;

namespace MusicInside.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistManager _artistManager;
        public ArtistController(IArtistManager manager)
        {
            _artistManager = manager;
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
