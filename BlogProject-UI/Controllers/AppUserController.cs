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

            var viewModel = new UserProfileViewModel
            {
                AppUserDTO = userDTO,
                UserPasswordChangeDTO = new UserPasswordChanceDTO()
            };

            ViewBag.ShowSidebar = false;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserSettings(UserProfileViewModel model)
        {
            var userId = _service.UserManager.GetUserId(HttpContext.User);
            var user = await _service.AppUserService.GetAppUserByIdAsync(userId);

            if (ModelState.IsValid)
            {
                user.UserName = model.AppUserDTO.UserName;
                user.Email = model.AppUserDTO.Email;
                user.PhoneNumber = model.AppUserDTO.PhoneNumber;

                await _service.AppUserService.UpdateAppUserAsync(user);

                ViewBag.ShowSidebar = false;
                return RedirectToAction("UserSettings");
            }

            ViewBag.ShowSidebar = false;
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
            var comments = await _service.CommentService.GetAllCommentsByUserIdAsync(user.Id);

            ViewBag.ShowSidebar = false;
            return View(comments);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserPasswordChanceDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View("UserSettings", new UserProfileViewModel
                {
                    AppUserDTO = new AppUserDTO(),
                    UserPasswordChangeDTO = model
                });
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
                return View("UserSettings", new UserProfileViewModel
                {
                    AppUserDTO = new AppUserDTO
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Photo = user.Photo
                    },
                    UserPasswordChangeDTO = model
                });
            }

            return RedirectToAction("UserSettings");
        }
    }
}