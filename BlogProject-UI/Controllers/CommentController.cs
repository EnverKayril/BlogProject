using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.Controllers
{
    public class CommentController : Controller
    {
        private readonly IUnitOfWorkService _service;

        public CommentController(IUnitOfWorkService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var comment = await _service.CommentService.GetCommentByIdAsync(id);
                if (comment == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var currentUserId = _service.UserManager.GetUserId(User);
                if (comment.AppUserId != currentUserId)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 403 });
                }
                return View(comment);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.Edit (GET)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, CommentDTO model)
        {
            try
            {
                var comment = await _service.CommentService.GetCommentByIdAsync(id);

                if (comment == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var currentUserId = _service.UserManager.GetUserId(User);
                if (comment.AppUserId != currentUserId)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 403 });
                }

                if (ModelState.IsValid)
                {
                    comment.Content = model.Content;
                    await _service.CommentService.UpdateCommentAsync(comment);
                    return RedirectToAction("UserComments", "AppUser");
                }
                return View(comment);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.Edit (POST)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var comment = await _service.CommentService.GetCommentByIdAsync(id);
                if (comment == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var currentUserId = _service.UserManager.GetUserId(User);
                if (comment.AppUserId != currentUserId)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 403 });
                }

                return View(comment);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.Delete (GET)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(string id, CommentDTO model)
        {
            try
            {
                var comment = await _service.CommentService.GetCommentByIdAsync(id);
                if (comment == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var currentUserId = _service.UserManager.GetUserId(User);
                if (comment.AppUserId != currentUserId)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 403 });
                }

                await _service.CommentService.DeleteCommentAsync(id);
                return RedirectToAction("UserComments", "AppUser");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.Delete (POST)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }
    }
}
