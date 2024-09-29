using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Utilities.Extensions;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Models.VMs;
using Microsoft.AspNetCore.Authorization;
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
            try
            {
                var article = await _service.ArticleService.GetArticleByIdAsync(id);
                if (article == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                article.ViewCount += 1;
                await _service.ArticleService.UpdateArticleAsync(article);

                var user = await _service.AppUserService.GetAppUserByIdAsync(article.AppUserId);
                var comments = await _service.CommentService.GetAllCommentsByArticleIdAsync(id);
                var category = await _service.CategoryService.GetCategoryByIdAsync(article.CategoryId);

                if (category == null || user == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

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
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in ArticleController.Detail");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var article = await _service.ArticleService.GetArticleByIdAsync(id);
                if (article == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var currentUserId = _service.UserManager.GetUserId(User);
                if (article.AppUserId != currentUserId)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 403 });
                }

                return View(article);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in ArticleController.Edit (GET)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, ArticleCreateDTO model, IFormFile? photo)
        {
            try
            {
                var article = await _service.ArticleService.GetArticleByIdAsync(id);

                if (article == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var currentUserId = _service.UserManager.GetUserId(User);
                if (article.AppUserId != currentUserId)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 403 });
                }

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
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in ArticleController.Edit (POST)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var article = await _service.ArticleService.GetArticleByIdAsync(id);
                if (article == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var currentUserId = _service.UserManager.GetUserId(User);
                if (article.AppUserId != currentUserId)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 403 });
                }

                return View(article);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in ArticleController.Delete (GET)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(string id, ArticleDTO model)
        {
            try
            {
                var article = await _service.ArticleService.GetArticleByIdAsync(id);

                if (article == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var currentUserId = _service.UserManager.GetUserId(User);
                if (article.AppUserId != currentUserId)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 403 });
                }

                await _service.ArticleService.DeleteArticleAsync(id);
                return RedirectToAction("UserArticles", "AppUser");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in ArticleController.Delete (POST)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }
    }
}
