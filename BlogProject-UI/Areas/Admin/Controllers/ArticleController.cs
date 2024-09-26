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

        public async Task<IActionResult> Index()
        {
            var articles = await _service.ArticleService.GetAllArticlesWithUserAsync();

            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _service.CategoryService.GetAllCategoriesAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleCreateDTO articleCreateDTO)
        {
            var userId = _service.UserManager.GetUserId(User);

            if (articleCreateDTO.CategoryId == null || articleCreateDTO.CategoryId == "")
            {
                ModelState.AddModelError("CategoryId", "Lütfen bir kategori seçin.");
            }

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Key: {modelState.Key}, Error: {error.ErrorMessage}");
                    }
                }

                // Kategorileri yeniden yükleyin ve hata durumunda sayfayı geri gönderin
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

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var categories = await _service.CategoryService.GetAllCategoriesAsync();
            ViewBag.CategoryList = new SelectList(categories, "Id", "Name");
            var model = await _service.ArticleService.GetArticleByIdAsync(id.ToString());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ArticleCreateDTO model, IFormFile? photo)
        {
            var article = await _service.ArticleService.GetArticleByIdAsync(id.ToString());
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
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var model = await _service.ArticleService.GetArticleByIdAsync(id);
            var category = await _service.CategoryService.GetCategoryByIdAsync(model.CategoryId);
            ViewBag.CategoryName = category.Name;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, ArticleDTO model)
        {
            try
            {
                await _service.ArticleService.DeleteArticleAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}
