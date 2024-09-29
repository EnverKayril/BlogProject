using BlogProject.REPO.Utilities.UnitOfWork;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Editor")]
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
                var model = await _service.CategoryService.GetAllCategoriesAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CategoryController.Index");

                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var result = _service.CategoryService.CreateCategoryAsync(model);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CategoryController.Create");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var model = await _service.CategoryService.GetCategoryByIdAsync(id);
                if (model == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }
                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CategoryController.Edit (GET)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, CategoryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                _service.CategoryService.UpdateCategoryAsync(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CategoryController.Edit (POST)");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var model = await _service.CategoryService.GetCategoryByIdAsync(id);
                if (model == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }
                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CategoryController.Delete (GET)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id, CategoryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _service.CategoryService.DeleteCategoryAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in CategoryController.Delete (POST)");

                return View(model);
            }
        }
    }
}
