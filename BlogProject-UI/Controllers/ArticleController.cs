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
            var article = await _service.ArticleService.GetArticleByIdAsync(id);

            var user = await _service.AppUserService.GetAppUserByIdAsync(article.AppUserId);
            var comments = await _service.CommentService.GetAllCommentsByArticleIdAsync(id);
            var category = await _service.CategoryService.GetCategoryByIdAsync(article.CategoryId);

            var model = new ArticleDetailViewModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Thumbnail = article.Thumbnail,
                CreateDate = article.CreateDate,
                UserId = user.Id,
                UserName = user?.UserName,
                UserProfilePicture = user?.Photo,
                CategoryId = article.CategoryId,
                CategoryName = category.Name,
                CommentCount = comments.Count(),
                ViewsCount = article.ViewCount,
                Comments = comments.ToList()
            };

            return View(model);
        }
    }
}
