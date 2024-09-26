using BlogProject.REPO.Utilities.Extensions;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Models.VMs;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.Controllers
{
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
            var users = await _service.AppUserService.GetAllAppUserAsync();
            return View(users);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _service.AppUserService.GetAppUserByIdAsync(id);

            var articles = await _service.ArticleService.GetAllArticlesByUserIdAsync(id);

            var model = new UserWithArticlesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                UserPhoto = user.Photo,
                Articles = articles
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserSettings()
        {
            var user = await _service.UserManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToAction("Login", "Authorization");
            }

            var userDTO = new AppUserDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Photo = user.Photo
            };
            ViewBag.ShowSidebar = false;
            return View(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> UserSettings(AppUserDTO model)
        {
            var userId = _service.UserManager.GetUserId(HttpContext.User);
            var user = await _service.AppUserService.GetAppUserByIdAsync(userId);

            if (ModelState.IsValid)
            {
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                await _service.AppUserService.UpdateAppUserAsync(user);

                return RedirectToAction("UserSettings");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ViewResult> ChangePhoto()
        {
            var user = await _service.UserManager.GetUserAsync(HttpContext.User);
            var userDTO = new AppUserDTO
            {
                Photo = user.Photo
            };
            return View(userDTO);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePhoto(IFormFile? photo)
        {
            var user = await _service.UserManager.GetUserAsync(HttpContext.User);
            var oldPhoto = user.Photo;

            if (photo != null)
            {
                var newPhoto = await _imageHelper.ImageUpload(photo, "UserImages");

                if (oldPhoto != null && oldPhoto != "DefaultUser.jpg")
                {
                    _imageHelper.DeleteImage(oldPhoto, "UserImages");
                }

                user.Photo = newPhoto;
                await _service.UserManager.UpdateAsync(user);
            }

            return RedirectToAction("UserSettings", "AppUser");
        }

        [HttpGet]
        public async Task<IActionResult> UserArticles()
        {
            var user = await _service.UserManager.GetUserAsync(HttpContext.User);
            var articles = await _service.ArticleService.GetAllArticlesByUserIdAsync(user.Id);

            ViewBag.ShowSidebar = false;
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> UserComments()
        {
            var user = await _service.UserManager.GetUserAsync(HttpContext.User);
            var userComments = await _service.CommentService.GetCommentsWithArticleAndUserByIdAsync(user.Id);

            ViewBag.ShowSidebar = false;
            return View(userComments);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.ShowSidebar = false;
            return View(new UserPasswordChanceDTO());
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserPasswordChanceDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _service.UserManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToAction("Login", "Authorization");
            }

            var result = await _service.UserManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Şifre değiştirilemedi. Lütfen tekrar deneyin.");
                return View(model);
            }

            return RedirectToAction("UserSettings");
        }
    }
}