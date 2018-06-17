using Microsoft.AspNetCore.Mvc;

namespace MusicInside.WebApi.Controllers
{
    public class ArtistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}