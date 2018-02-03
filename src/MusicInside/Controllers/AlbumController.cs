using log4net;
using Microsoft.AspNetCore.Mvc;
using MusicInside.Exceptions;
using MusicInside.ManagerInterfaces;
using MusicInside.ModelView;
using System;
using System.Collections.Generic;

namespace MusicInside.Controllers
{
    public class AlbumController : Controller
    {
        #region Members
        private readonly IAlbumManager _albumManager;
        private readonly ILog _logger;
        #endregion

        public AlbumController(IAlbumManager albumManager, ILog logger)
        {
            _albumManager = albumManager;
            _logger = logger;
        }

        #region Page Resolvers
        public IActionResult Index()
        {
            try
            {
                List<AlbumRowViewModel> allAlbums = _albumManager.GetAllTable();
                return View(allAlbums);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("AlbumController | Index: Error occurred [{0}]", ex.Message);
                return RedirectToRoute("Error", "GenericError");
            }
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
                return RedirectToRoute("Error", "InvalidIdError");
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("AlbumController | Detail: Error occurred [{0}]", enpex.Message);
                return RedirectToRoute("Error", "EntryNotPresentError");
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("AlbumController | Detail: A generic error occurred [{0}]", ex.Message);
                return RedirectToRoute("Error", "GenericError");
            }
        }
        #endregion

        #region Controller Service Methods
        public ActionResult GetCoverImage(int id = -1)
        {
            try
            {
                byte[] imageBytes = _albumManager.GetAlbumCoverFile(id);
                return base.File(imageBytes, "image/png");
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("AlbumController | GetCoverImage: A generic error occurred [{0}]", ex.Message);
                return null;
            }
        }

        [HttpGet]
        public JsonResult GetAlbumList()
        {
            try
            {
                List<AlbumRowViewModel> albumList = _albumManager.GetAllTable();
                return Json(albumList);
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("AlbumController | GetAllAlbums: A generic error occurred [{0}]", ex.Message);
                return null;
            }
        }
        #endregion
    }
}
