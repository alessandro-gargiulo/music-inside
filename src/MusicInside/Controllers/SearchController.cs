using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MusicInside.Controllers
{
    public class SearchController : Controller
    {

        #region Page Resolvers
        public IActionResult Index()
        {
            return View();
        }
        #endregion
    }
}
