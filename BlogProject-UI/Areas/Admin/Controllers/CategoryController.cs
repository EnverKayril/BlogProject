using BlogProject.REPO.Utilities.UnitOfWork;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWorkService _service;

        public CategoryController(IUnitOfWorkService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _service.CategoryService.GetAllCategoriesAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateDTO model)
        {
            var result = _service.CategoryService.CreateCategoryAsync(model);
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _service.CategoryService.GetCategoryByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CategoryDTO model)
        {
            try
            {
                _service.CategoryService.UpdateCategoryAsync(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var model = await _service.CategoryService.GetCategoryByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id,CategoryDTO model)
        {
            try
            {
                await _service.CategoryService.DeleteCategoryAsync(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
