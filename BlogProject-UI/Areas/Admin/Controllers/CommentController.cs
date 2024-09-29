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

        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var comments = await _service.CommentService.GetCommentsWithArticleAndUserAsync();
                return View(comments);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.Index");
                return View(new List<CommentWithUserDTO>());
            }
        }

        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> UnApprovedComments()
        {
            try
            {
                var unApprovedComments = await _service.CommentService.GetCommentsWithArticleAndUserAsync(true);
                return View(unApprovedComments);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.UnApprovedComments");
                return View(new List<CommentWithUserDTO>());
            }
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> UnApprovedCommentsGetDetail(string id)
        {
            try
            {
                var unApprovedComment = await _service.CommentService.GetCommentByIdWithArticleAndUserAsync(id);

                if (unApprovedComment == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var comment = new CommentWithUserDTO
                {
                    CommentId = unApprovedComment.CommentId,
                    CommentContent = unApprovedComment.CommentContent,
                    CreateDate = unApprovedComment.CreateDate,
                    Approved = unApprovedComment.Approved,
                    UserName = unApprovedComment.UserName,
                    ArticleTitle = unApprovedComment.ArticleTitle
                };

                return View(comment);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.UnApprovedCommentsGetDetail");
                return View(new CommentWithUserDTO());
            }
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPost]
        public async Task<IActionResult> UnApprovedCommentsGetDetail(string id, CommentWithUserDTO model, string action)
        {
            try
            {
                var comment = await _service.CommentService.GetCommentByIdAsync(id);
                if (comment == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                if (action == "Onayla")
                {
                    comment.Approved = true;
                    await _service.CommentService.ApproveCommentAsync(comment.Id);
                }
                else if (action == "Sil")
                {
                    await _service.CommentService.DeleteCommentAsync(id);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.UnApprovedCommentsGetDetail (POST)");
                return View(model);
            }
        }

        [Authorize(Roles = "Admin, Editor, Verifieduser")]
        [HttpPost]
        public async Task<IActionResult> Create(string ArticleId, string UserId, string Message)
        {
            try
            {
                if (string.IsNullOrEmpty(ArticleId) || string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(Message))
                {
                    return RedirectToAction("Detail", "Article", new { area = "", id = ArticleId });
                }

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
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.Create (POST)");
                return RedirectToAction("Detail", "Article", new { area = "", id = ArticleId });
            }
        }

        [Authorize(Roles = "Admin, Editor")]
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
                return View(comment);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.Delete (GET)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize(Roles = "Admin, Editor")]
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
                await _service.CommentService.DeleteCommentAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.Delete (POST)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize(Roles = "Admin, Editor")]
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
                return View(comment);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.Edit (GET)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize(Roles = "Admin, Editor")]
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
                comment.Approved = false;
                comment.Content = model.Content;

                await _service.CommentService.UpdateCommentAsync(comment);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CommentController.Edit (POST)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }
    }
}
