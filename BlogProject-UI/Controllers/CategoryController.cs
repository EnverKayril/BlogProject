using BlogProject.SERVICE.Utilities.IUnitOfWorks;
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
    }
}