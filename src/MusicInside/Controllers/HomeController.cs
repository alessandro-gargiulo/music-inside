using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MusicInside.Controllers
{
    public class HomeController : Controller
    {
        #region Page Resolvers
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Here you can find my contacts.";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        #endregion
    }
}
