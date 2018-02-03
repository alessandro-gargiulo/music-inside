using log4net;
using Microsoft.AspNetCore.Mvc;
using MusicInside.Exceptions;
using MusicInside.ManagerInterfaces;
using MusicInside.ModelView;
using System;
using System.Collections.Generic;

namespace MusicInside.Controllers
{
    public class ArtistController : Controller
    {
        #region Members
        private readonly IArtistManager _artistManager;
        private readonly ILog _logger;
        #endregion

        public ArtistController(IArtistManager artistManager, ILog logger)
        {
            _artistManager = artistManager;
            _logger = logger;
        }

        #region Page Resolvers
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("ArtistController | Index: Error occurred [{0}]", ex.Message);
                return RedirectToRoute("Error", "GenericError");
            }
        }

        public IActionResult Detail(int id = -1)
        {
            try
            {
                ArtistDetailViewModel artistDetail = _artistManager.GetDetailById(id);
                return View(artistDetail);
            }
            catch (InvalidIdException iiex)
            {
                _logger.ErrorFormat("ArtistController | Detail: Error occurred [{0}]", iiex.Message);
                return RedirectToRoute("Error", "InvalidIdError");
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("ArtistController | Detail: Error occurred [{0}]", enpex.Message);
                return RedirectToRoute("Error", "EntryNotPresentError");
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("ArtistController | Detail: A generic error occurred [{0}]", ex.Message);
                return RedirectToRoute("Error", "GenericError");
            }
        }
        #endregion

        #region Controller Service Methods
        [HttpGet]
        public JsonResult GetArtistList()
        {
            List<ArtistRowViewModel> artistList = new List<ArtistRowViewModel>();
            try
            {
                artistList = _artistManager.GetAllTable();
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("ArtistController | GetArtistList: A generic error occurred [{0}]", ex.Message);
                return Json(-1);
            }
            return Json(artistList);
        }
        #endregion
    }
}
