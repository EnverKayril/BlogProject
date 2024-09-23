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
            var categories = await _service.CategoryService.GetAllCategoriesAsync();
            var sortedcategories = categories.OrderBy(c => c.Name);
            return View(sortedcategories);
        }

        public async Task<IActionResult> Detail(string Id)
        {
            var category = await _service.CategoryService.GetCategoryByIdAsync(Id);

            var articles = await _service.ArticleService.GetArticlesForHomePageAsync(Id);

            var model = new CategoryDetailViewModel
            {
                Category = category,
                Articles = articles
            };

            return View(model);
        }

    }
}