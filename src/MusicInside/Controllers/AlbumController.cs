using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicInside.ManagerInterfaces;
using log4net;
using MusicInside.Exceptions;

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

        public ActionResult GetCoverImage(int id = -1)
        {
            try
            {
                byte[] imageBytes = _albumManager.GetAlbumCoverFile(id);
                return base.File(imageBytes, "image/jpg");
            }
            catch (InvalidIdException iiex)
            {
                _logger.Error("AlbumController | GetCoverImage: " + iiex.Message);
                return null;
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.Error("AlbumController | GetCoverImage: " + enpex.Message);
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error("AlbumController | GetCoverImage: A generic error occurred " + ex.Message);
                return null;
            }

        }
    }
}
