using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWorkService;

        public CategoryController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWorkService = unitOfWorkService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _unitOfWorkService.CategoryService.GetAllCategoriesAsync();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateDTO model)
        {
            var result = _unitOfWorkService.CategoryService.CreateCategoryAsync(model);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            return View(result);
        }
    }
}
