using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Models.VMs;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IUnitOfWorkService _service;

        public ArticleController(IUnitOfWorkService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Detail(string id)
        {
            //var articleDetail = await _service.ArticleService.GetArticleDetailByIdAsync(id);
            //var comments = await _service.CommentService.GetAllCommentsByArticleIdAsync(id);

            //articleDetail.Comments = comments;
            //articleDetail.NewComment = new CommentCreateDTO { ArticleId = id };

            //return View(articleDetail);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentCreateDTO comment)
        {
            if (ModelState.IsValid)
            {
                await _service.CommentService.CreateCommentAsync(comment);
                return RedirectToAction("Detail", new { id = comment.ArticleId });
            }

            var article = await _service.ArticleService.GetArticleByIdAsync(comment.ArticleId);
            var comments = await _service.CommentService.GetAllCommentsByArticleIdAsync(comment.ArticleId);
            var viewModel = new ArticleDetailViewModel
            {
                Article = article,
                Comments = comments,
                NewComment = comment
            };

            return View("Detail", viewModel);
        }

    }
}
