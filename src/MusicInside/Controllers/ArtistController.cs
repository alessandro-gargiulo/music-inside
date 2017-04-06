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
        private readonly IArtistManager _artistManager;
        private readonly ILog _logger;

        public ArtistController(IArtistManager artistManager, ILog logger)
        {
            _artistManager = artistManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                List<ArtistRowViewModel> allArtists = _artistManager.GetAllTable();
                return View(allArtists);
            }
            catch (Exception ex)
            {
                _logger.Error("ArtistController | Index: " + ex.Message);
                return null;
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
                _logger.Error("ArtistController | Detail: " + iiex.Message);
                return null; // Redirect to error screen
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.Error("ArtistController | Detail: " + enpex.Message);
                return null; // Redirect to error screen
            }
            catch (Exception ex)
            {
                _logger.Error("ArtistController | Detail: A generic error occurred " + ex.Message);
                return null;
            }
        }
    }
}
