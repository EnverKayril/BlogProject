using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Utilities.Extensions;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Models.VMs;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IUnitOfWorkService _service;
        private readonly ImageHelper _imageHelper;

        public ArticleController(IUnitOfWorkService service, ImageHelper imageHelper)
        {
            _service = service;
            _imageHelper = imageHelper;
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

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _service.ArticleService.GetArticleByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ArticleCreateDTO model, IFormFile? photo)
        {
            var article = await _service.ArticleService.GetArticleByIdAsync(id);
            var oldPhoto = article.Thumbnail;
            if (ModelState.IsValid)
            {
                if (photo == null)
                {
                    model.Thumbnail = oldPhoto;
                }
                else
                {
                    model.Thumbnail = await _imageHelper.ImageUpload(photo, "ArticleImages");

                    if (oldPhoto != null && oldPhoto != "DefaultThumbnail.jpg")
                    {
                        _imageHelper.DeleteImage(oldPhoto, "ArticleImages");
                    }
                }

                article.Title = model.Title;
                article.Content = model.Content;
                article.Thumbnail = model.Thumbnail;

                await _service.ArticleService.UpdateArticleAsync(article);
                return RedirectToAction("UserArticles", "AppUser");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var article = await _service.ArticleService.GetArticleByIdAsync(id);
            return View(article);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, ArticleDTO model)
        {
            try
            {
                await _service.ArticleService.DeleteArticleAsync(id);
                return RedirectToAction("UserArticles", "AppUser");
            }
            catch
            {
                return View(model);
            }
        }

    }
}
