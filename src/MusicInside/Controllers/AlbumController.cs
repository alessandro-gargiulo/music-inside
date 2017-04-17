using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicInside.ManagerInterfaces;
using log4net;
using MusicInside.Exceptions;
using MusicInside.ModelView;

namespace MusicInside.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumManager _albumManager;
        private readonly ILog _logger;

        public AlbumController(IAlbumManager albumManager, ILog logger)
        {
            _albumManager = albumManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id = -1)
        {
            try
            {
                AlbumDetailViewModel albumDetail = _albumManager.GetDetailById(id);
                return View(albumDetail);
            }
            catch (InvalidIdException iiex)
            {
                _logger.ErrorFormat("AlbumController | Detail: Error occurred [{0}]", iiex.Message);
                return null; // Redirect to error screen
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("AlbumController | Detail: Error occurred [{0}]", enpex.Message);
                return null; // Redirect to error screen
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("AlbumController | Detail: A generic error occurred [{0}]", ex.Message);
                return null;
            }
        }

        public ActionResult GetCoverImage(int id = -1)
        {
            try
            {
                byte[] imageBytes = _albumManager.GetAlbumCoverFile(id);
                return base.File(imageBytes, "image/png");
            }
            catch (InvalidIdException iiex)
            {
                _logger.ErrorFormat("AlbumController | GetCoverImage: Error occurred [{0}]", iiex.Message);
                return null; // Redirect to error screen
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("AlbumController | GetCoverImage: Error occurred [{0}]", enpex.Message);
                return null; // Redirect to error screen
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("AlbumController | GetCoverImage: A generic error occurred [{0}]", ex.Message);
                return null;
            }
        }
    }
}
