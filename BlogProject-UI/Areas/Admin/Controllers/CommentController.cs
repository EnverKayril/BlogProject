using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly IUnitOfWorkService _service;

        public CommentController(IUnitOfWorkService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var comments = await _service.CommentService.GetCommentsWithArticleAndUserAsync();

            return View(comments);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string ArticleId, string UserId, string Message)
        {
            var comment = new CommentCreateDTO
            {
                Id = Guid.NewGuid().ToString(),
                ArticleId = ArticleId,
                AppUserId = UserId,
                Content = Message,
                CreateDate = DateTime.Now,
                Approved = false
            };

            await _service.CommentService.CreateCommentAsync(comment);

            return RedirectToAction("Detail", "Article", new { area = "", id = ArticleId });
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
            await _service.CommentService.DeleteCommentAsync(id);
            return RedirectToAction("Index");
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

            comment.Approved = false;
            comment.Content = model.Content;

            await _service.CommentService.UpdateCommentAsync(comment);
            return RedirectToAction("Index");
        }

    }
}
