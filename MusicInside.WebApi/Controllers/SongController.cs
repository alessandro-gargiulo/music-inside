using Microsoft.AspNetCore.Mvc;

namespace MusicInside.WebApi.Controllers
{
    public class SongController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}