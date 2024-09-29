using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Models.VMs;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWorkService _service;

        public CategoryController(IUnitOfWorkService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _service.CategoryService.GetAllCategoriesAsync();
                var sortedCategories = categories.OrderBy(c => c.Name);
                return View(sortedCategories);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CategoryController.Index");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        public async Task<IActionResult> Detail(string id)
        {
            try
            {
                var category = await _service.CategoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var articles = await _service.ArticleService.GetArticlesForHomePageAsync(id);
                var model = new CategoryDetailViewModel
                {
                    Category = category,
                    Articles = articles
                };
                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CategoryController.Detail");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }
    }
}