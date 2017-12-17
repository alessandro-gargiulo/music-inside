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
