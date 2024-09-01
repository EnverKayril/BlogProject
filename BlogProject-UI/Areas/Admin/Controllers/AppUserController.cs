using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Utilities.Extensions;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Mappers;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProject_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppUserController : Controller
    {
        private readonly IUnitOfWorkService _service;
        private readonly ImageHelper _imageHelper;

        public AppUserController(IUnitOfWorkService service, ImageHelper imageHelper)
        {
            _service = service;
            _imageHelper = imageHelper;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _service.AppUserService.GetAllAppUserAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppUserCreateDTO model)
        {
            var appUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Photo = await _imageHelper.ImageUpload(model.PhotoFile)
            };

            var result = await _service.UserManager.CreateAsync(appUser, model.Password);
            if (result is not null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _service.AppUserService.GetAppUserByIdAsync(id.ToString());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, AppUserDTO model, IFormFile? photo)
        {
            var user = await _service.AppUserService.GetAppUserByIdAsync(id.ToString());

            if (ModelState.IsValid)
            {
                if (photo == null)
                {
                    model.Photo = user.Photo;
                }
                else
                {
                    model.Photo = await _imageHelper.ImageUpload(photo);
                }

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Photo = model.Photo;

                await _service.AppUserService.UpdateAppUserAsync(user);
                return RedirectToAction("Index");

            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var model = await _service.AppUserService.GetAppUserByIdAsync(id.ToString());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, AppUserDTO model)
        {
            try
            {
                await _service.AppUserService.DeleteAppUser(id.ToString());
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}