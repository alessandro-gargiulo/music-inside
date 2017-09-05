using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicInside.ManagerInterfaces;
using log4net;
using MusicInside.ModelView;
using MusicInside.Exceptions;

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
                return null; // Redirect to error screen
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
                return null; // Redirect to error screen
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("ArtistController | Detail: Error occurred [{0}]", enpex.Message);
                return null; // Redirect to error screen
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("ArtistController | Detail: A generic error occurred [{0}]", ex.Message);
                return null;
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
