using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Utilities.Extensions;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Mappers;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [Authorize(Roles = "Admin, Editor")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await _service.AppUserService.GetAllAppUserAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                var logger = NLog.LogManager.GetCurrentClassLogger();
                logger.Error(ex, "Error occurred in AppUserController.Index");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetDetail(string id)
        {
            try
            {
                var appUser = await _service.AppUserService.GetAppUserByIdAsync(id);

                if (appUser == null)
                {
                    return View(new AppUserDTO());
                }
                return View(appUser);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.GetDetail");
                return View(new AppUserDTO());
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(AppUserCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var appUser = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Photo = await _imageHelper.ImageUpload(model.PhotoFile, "UserImages")
                };

                var result = await _service.UserManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.Create");
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var model = await _service.AppUserService.GetAppUserByIdAsync(id);

                if (model == null)
                {
                    return RedirectToAction("Error", "Error", new { statusCode = 404 });
                }
                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.Edit (GET)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, AppUserDTO model, IFormFile? photo)
        {
            try
            {
                var user = await _service.AppUserService.GetAppUserByIdAsync(id);
                if (user == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var oldPhoto = user.Photo;

                if (ModelState.IsValid)
                {
                    if (photo == null)
                    {
                        model.Photo = oldPhoto;
                    }
                    else
                    {
                        model.Photo = await _imageHelper.ImageUpload(photo, "UserImages");

                        if (!string.IsNullOrEmpty(oldPhoto) && oldPhoto != "DefaultUser.jpg")
                        {
                            _imageHelper.DeleteImage(oldPhoto, "UserImages");
                        }
                    }

                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Photo = model.Photo;
                    await _service.AppUserService.UpdateAppUserAsync(user);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.Edit (POST)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ChangeDetails()
        {
            var user = await _service.UserManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                NLog.LogManager.GetCurrentClassLogger().Error("User not found in ChangeDetails (GET)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
            }

            var userDTO = new AppUserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Photo = user.Photo
            };
            return View(userDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ChangeDetails(AppUserDTO model, IFormFile? photo)
        {
            var user = await _service.UserManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                NLog.LogManager.GetCurrentClassLogger().Error("User not found in ChangeDetails (POST)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
            }

            var oldPhoto = user.Photo;

            if (ModelState.IsValid)
            {
                try
                {
                    if (photo != null)
                    {
                        model.Photo = await _imageHelper.ImageUpload(photo, "UserImages");

                        if (oldPhoto != null && oldPhoto != "DefaultUser.jpg")
                        {
                            _imageHelper.DeleteImage(oldPhoto, "UserImages");
                        }
                        else
                        {
                            return View(model);
                        }
                    }
                    else
                    {
                        model.Photo = oldPhoto;
                    }

                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Photo = model.Photo;

                    await _service.UserManager.UpdateAsync(user);
                    NLog.LogManager.GetCurrentClassLogger().Info("User details updated successfully for user: " + user.UserName);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred while updating user details");
                    return View(model);
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _service.AppUserService.GetAppUserByIdAsync(id);

                if (user == null)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error($"User with ID {id} not found in Delete (GET)");
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                return View(user);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, $"Error occurred in Delete (GET) for User ID: {id}");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id, AppUserDTO model)
        {
            try
            {
                var user = await _service.UserManager.FindByIdAsync(id);

                if (user == null)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error($"User with ID {id} not found in Delete (POST)");
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var result = await _service.UserManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error($"Error occurred while deleting user with ID: {id}");
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
                }

                if (user.Photo != "DefaultUser.jpg")
                {
                    _imageHelper.DeleteImage(user.Photo, "UserImages");
                }

                NLog.LogManager.GetCurrentClassLogger().Info($"User with ID {id} successfully deleted");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, $"Error occurred in Delete (POST) for User ID: {id}");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UserSettings()
        {
            try
            {
                var user = await _service.UserManager.GetUserAsync(HttpContext.User);

                if (user == null)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error("User not found in UserSettings.");
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var userDTO = new AppUserDTO
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Photo = user.Photo
                };

                NLog.LogManager.GetCurrentClassLogger().Info($"User settings accessed for User ID: {user.Id}");
                return View(userDTO);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in UserSettings.");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }
    }
}