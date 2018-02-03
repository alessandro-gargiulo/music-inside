using log4net;
using Microsoft.AspNetCore.Mvc;

namespace MusicInside.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILog _logger;

        public ErrorController(ILog logger)
        {
            _logger = logger;
        }

        public IActionResult InvalidIdError()
        {
            return View();
        }

        public IActionResult EntryNotPresentError()
        {
            return View();
        }

        public IActionResult GenericError()
        {
            return View();
        }
    }
}
