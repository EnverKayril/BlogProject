﻿using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Utilities.Extensions;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Mappers;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var model = await _service.AppUserService.GetAllAppUserAsync();
            return View(model);
        }

        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _service.SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _service.UserManager.FindByEmailAsync(userLoginDTO.Email);
                if (user != null)
                {
                    var result = await _service.SignInManager.PasswordSignInAsync(user, userLoginDTO.Password, userLoginDTO.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-Posta adresiniz veya şifreniz yanlış.");
                        return View("Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-Posta adresiniz veya şifreniz yanlış.");
                    return View("Login");
                }
            }
            else
            {
                return View("Login");
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
            var appUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Photo = await _imageHelper.ImageUpload(model.PhotoFile, "UserImages")
            };

            var result = await _service.UserManager.CreateAsync(appUser, model.Password);
            if (result is not null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var model = await _service.AppUserService.GetAppUserByIdAsync(id.ToString());
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, AppUserDTO model, IFormFile? photo)
        {
            var user = await _service.AppUserService.GetAppUserByIdAsync(id.ToString());
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

                    if (oldPhoto != null && oldPhoto != "DefaultUser.jpg")
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
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<ViewResult> ChangeDetails()
        {
            var user = await _service.UserManager.GetUserAsync(HttpContext.User);
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

                    if (oldPhoto != null && oldPhoto != "DefaultUser.jpg")
                    {
                        _imageHelper.DeleteImage(oldPhoto, "UserImages");
                    }
                }

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Photo = model.Photo;

                //await _service.AppUserService.UpdateAppUserAsync(user);
                await _service.UserManager.UpdateAsync(user);
                return RedirectToAction("Index");

            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ViewResult PasswordChange()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChanceDTO userPasswordChanceDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _service.UserManager.GetUserAsync(HttpContext.User);
                var isVerified = await _service.UserManager.CheckPasswordAsync(user, userPasswordChanceDTO.CurrentPassword);
                if (isVerified)
                {
                    var result = await _service.UserManager.ChangePasswordAsync(user, userPasswordChanceDTO.CurrentPassword, userPasswordChanceDTO.NewPassword);
                    if (result.Succeeded)
                    {
                        await _service.UserManager.UpdateSecurityStampAsync(user);
                        await _service.SignInManager.SignOutAsync();
                        await _service.SignInManager.PasswordSignInAsync(user, userPasswordChanceDTO.NewPassword, true, false);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(userPasswordChanceDTO);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Lütfen girmiş olduğunuz şifrenizi kontrol ediniz.");
                    return View(userPasswordChanceDTO);
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var model = await _service.AppUserService.GetAppUserByIdAsync(id.ToString());
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id, AppUserDTO model)
        {
            try
            {
                var user = await _service.UserManager.FindByIdAsync(id);

                await _service.UserManager.DeleteAsync(user);
                if (user.Photo != "DefaultUser.jpg")
                {
                    _imageHelper.DeleteImage(user.Photo, "UserImages");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}