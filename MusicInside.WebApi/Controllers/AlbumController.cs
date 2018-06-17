using log4net;
using Microsoft.AspNetCore.Mvc;
using MusicInside.Managers.Implementations;
using MusicInside.WebApi.Shared;
using MusicInside.Managers.Exceptions;
using System;
using System.Net;

namespace MusicInside.WebApi.Controllers
{
    [Route(WebConstants.ROUTES.ALBUM_ROUTE)]
    public class AlbumController : Controller
    {
        private AlbumManager _albumManager;
        private ILog _logger;

        public AlbumController(AlbumManager albumManager, ILog logger)
        {
            _albumManager = albumManager;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult GetCoverImage(int id = -1)
        {
            try
            {
                byte[] imageBytes = _albumManager.GetCoverFile(id);
                return base.File(imageBytes, "image/png");
            }
            catch (InvalidIdException iidex)
            {
                _logger.ErrorFormat("AlbumController | GetCoverImage: An error occurred [{0}]", iidex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { status = (int)HttpStatusCode.BadRequest, message = iidex.Message });
            }
            catch (EntryNotPresentException enpex)
            {
                _logger.ErrorFormat("AlbumController | GetCoverImage: An error occurred [{0}]", enpex.Message);
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return Json(new { status = (int)HttpStatusCode.NotFound, message = enpex.Message });
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("AlbumController | GetCoverImage: A generic error occurred [{0}]", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { status = (int)HttpStatusCode.InternalServerError, message = "The server can't process your request at this time" });
            }
        }
    }
}