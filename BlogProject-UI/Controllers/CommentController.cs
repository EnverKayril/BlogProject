using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
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

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var comment = await _service.CommentService.GetCommentByIdAsync(id);
            return View(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CommentDTO model)
        {
            var comment = await _service.CommentService.GetCommentByIdAsync(id);

            if (ModelState.IsValid)
            {
                comment.Content = model.Content;
                await _service.CommentService.UpdateCommentAsync(comment);
                return RedirectToAction("UserComments", "AppUser");
            }

            return View(comment);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var comment = await _service.CommentService.GetCommentByIdAsync(id);
            return View(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, CommentDTO model)
        {
            try
            {
                await _service.CommentService.DeleteCommentAsync(id);
                return RedirectToAction("UserComments", "AppUser");
            }
            catch
            {
                return View(model);
            }
        }
    }
}
