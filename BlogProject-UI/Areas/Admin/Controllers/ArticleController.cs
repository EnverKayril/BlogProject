using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Utilities.Extensions;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogProject_UI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly IUnitOfWorkService _service;
        private readonly ImageHelper _imageHelper;

        public ArticleController(IUnitOfWorkService service, ImageHelper imageHelper)
        {
            _service = service;
            _imageHelper = imageHelper;
        }

        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var articles = await _service.ArticleService.GetAllArticlesWithUserAsync();
                return View(articles);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in ArticleController.Index");
                return View(new List<ArticleDTO>());
            }
        }

        [Authorize(Roles = "Admin, Editor, Verifieduser")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var categories = await _service.CategoryService.GetAllCategoriesAsync();
                ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
                return View();
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in ArticleController.Create (GET)");
                return View();
            }
        }

        [Authorize(Roles = "Admin, Editor, Verifieduser")]
        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateDTO articleCreateDTO)
        {
            try
            {
                var userId = _service.UserManager.GetUserId(User);

                if (string.IsNullOrEmpty(articleCreateDTO.CategoryId))
                {
                    var categories = await _service.CategoryService.GetAllCategoriesAsync();
                    ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
                    return View(articleCreateDTO);
                }

                var article = new Article
                {
                    Id = Guid.NewGuid().ToString(),
                    CreateDate = DateTime.Now,
                    Status = BlogProject.CORE.CoreModels.Enums.EntityStatus.Created,
                    ViewsCount = 0,
                    CommentCount = 0,
                    AppUserId = userId,
                    Title = articleCreateDTO.Title,
                    Content = articleCreateDTO.Content,
                    CategoryId = articleCreateDTO.CategoryId,
                    Thumbnail = await _imageHelper.ImageUpload(articleCreateDTO.PhotoFile, "ArticleImages")
                };

                await _service.ArticleService.CreateArticleAsync(article);
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in ArticleController.Create (POST)");

                var categories = await _service.CategoryService.GetAllCategoriesAsync();
                ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
                return View(articleCreateDTO);
            }
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var categories = await _service.CategoryService.GetAllCategoriesAsync();
                ViewBag.CategoryList = new SelectList(categories, "Id", "Name");

                var model = await _service.ArticleService.GetArticleByIdAsync(id.ToString());
                if (model == null)
                {
                    NLog.LogManager.GetCurrentClassLogger().Warn($"Makale bulunamadı. Id: {id}");
                    return RedirectToAction("Index", "Error", new { statusCode = 404 });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in ArticleController.Edit (GET)");

                return RedirectToAction("Error", "Error", new { statusCode = 500 });
            }
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, ArticleCreateDTO model, IFormFile? photo)
        {
            try
            {
                var article = await _service.ArticleService.GetArticleByIdAsync(id.ToString());
                if (article == null)
                {
                    NLog.LogManager.GetCurrentClassLogger().Warn($"Makale bulunamadı. Id: {id}");
                    return RedirectToAction("Index", "Error", new { statusCode = 404 });
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
                    article.CategoryId = model.CategoryId;
                    article.Thumbnail = model.Thumbnail;

                    await _service.ArticleService.UpdateArticleAsync(article);
                    NLog.LogManager.GetCurrentClassLogger().Info($"Makale başarıyla güncellendi. Id: {id}");

                    return RedirectToAction("Index");
                }

                var categories = await _service.CategoryService.GetAllCategoriesAsync();
                ViewBag.CategoryList = new SelectList(categories, "Id", "Name");

                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in ArticleController.Edit (POST)");

                var categories = await _service.CategoryService.GetAllCategoriesAsync();
                ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
                return View(model);
            }
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var model = await _service.ArticleService.GetArticleByIdAsync(id);
                if (model == null)
                {
                    NLog.LogManager.GetCurrentClassLogger().Warn($"Makale bulunamadı. Id: {id}");
                    return RedirectToAction("Error", "Error", new { statusCode = 404 });
                }

                var category = await _service.CategoryService.GetCategoryByIdAsync(model.CategoryId);
                if (category == null)
                {
                    NLog.LogManager.GetCurrentClassLogger().Warn($"Kategori bulunamadı. CategoryId: {model.CategoryId}");
                    ViewBag.CategoryName = "Kategori bulunamadı";
                }
                else
                {
                    ViewBag.CategoryName = category.Name;
                }

                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in ArticleController.Delete (GET)");

                return RedirectToAction("Error", "Error", new { statusCode = 500 });
            }
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id, ArticleDTO model)
        {
            try
            {
                var article = await _service.ArticleService.GetArticleByIdAsync(id);
                if (article == null)
                {
                    NLog.LogManager.GetCurrentClassLogger().Warn($"Silinmek istenen makale bulunamadı. Id: {id}");
                    return RedirectToAction("Error", "Error", new { statusCode = 404 });
                }

                await _service.ArticleService.DeleteArticleAsync(id);

                NLog.LogManager.GetCurrentClassLogger().Info($"Makale başarıyla silindi. Id: {id}");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in ArticleController.Delete (POST)");
                return View(model);
            }
        }
    }
}
