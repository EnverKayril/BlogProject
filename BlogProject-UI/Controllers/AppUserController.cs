using BlogProject.REPO.Utilities.Extensions;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Models.VMs;
using Microsoft.AspNetCore.Authorization;
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
            try
            {
                var users = await _service.AppUserService.GetAllAppUserAsync();
                return View(users);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.Index");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UserSettings()
        {
            try
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
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.UserSettings");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserSettings(AppUserDTO model)
        {
            try
            {
                var userId = _service.UserManager.GetUserId(HttpContext.User);
                var user = await _service.AppUserService.GetAppUserByIdAsync(userId);
                if (user == null)
                {
                    return RedirectToAction("Login", "Authorization");
                }
                if (ModelState.IsValid)
                {
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.PhoneNumber = model.PhoneNumber;
                    ViewBag.ShowSidebar = false;
                    await _service.AppUserService.UpdateAppUserAsync(user);

                    return RedirectToAction("UserSettings");
                }
                ViewBag.ShowSidebar = false;
                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.UserSettings (POST)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChangePhoto()
        {
            try
            {
                var user = await _service.UserManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Authorization");
                }
                var userDTO = new AppUserDTO
                {
                    Photo = user.Photo
                };
                return View(userDTO);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.ChangePhoto (GET)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePhoto(IFormFile? photo)
        {
            try
            {
                var user = await _service.UserManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Authorization");
                }
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
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.ChangePhoto (POST)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UserArticles()
        {
            try
            {
                var user = await _service.UserManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Authorization");
                }

                var articles = await _service.ArticleService.GetAllArticlesByUserIdAsync(user.Id);
                ViewBag.ShowSidebar = false;
                return View(articles);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.UserArticles");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UserComments()
        {
            try
            {
                var user = await _service.UserManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Authorization");
                }
                var userComments = await _service.CommentService.GetCommentsWithArticleAndUserByIdAsync(user.Id);
                ViewBag.ShowSidebar = false;
                return View(userComments);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.UserComments");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.ShowSidebar = false;
            return View(new UserPasswordChanceDTO());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserPasswordChanceDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ShowSidebar = false;
                return View(model);
            }
            try
            {
                var user = await _service.UserManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Authorization");
                }
                var result = await _service.UserManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    ViewBag.ShowSidebar = false;
                    return View(model);
                }
                ViewBag.ShowSidebar = false;
                return RedirectToAction("UserSettings");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.ChangePassword");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult ConfirmEmail()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            try
            {
                var user = await _service.UserManager.GetUserAsync(HttpContext.User);
                var model = new ContactViewModel();

                if (user != null)
                {
                    model.UserName = user.UserName;
                    model.Email = user.Email;
                }
                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.Contact (GET)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Authorization");
                }

                if (ModelState.IsValid)
                {
                    string subject = model.Subject;
                    string message = $"Kullanıcı Adı: {model.UserName}\nE-posta: {model.Email}\n\nMesaj:\n{model.Message}";

                    await _service.EmailSender.SendEmailAsync("admin@example.com", subject, message);
                    return RedirectToAction("Contact");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.Contact (POST)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DeleteMyAccount()
        {
            var user = await _service.UserManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return RedirectToAction("Login", "Authorization");
            }
            ViewBag.ShowSidebar = false;
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteMyAccount(string userId, string Email, string Password)
        {
            try
            {
                // Kullanıcıyı al
                var user = await _service.UserManager.GetUserAsync(HttpContext.User);

                if (user == null || user.Id != userId)
                {
                    return RedirectToAction("Error", "Error", new { statusCode = 403 });
                }

                // E-posta doğrulaması
                if (!string.Equals(user.Email, Email, StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("", "E-posta adresi yanlış.");
                    return View(user);
                }

                // Şifre doğrulaması
                var passwordCheck = await _service.UserManager.CheckPasswordAsync(user, Password);
                if (!passwordCheck)
                {
                    ModelState.AddModelError("", "Şifre yanlış.");
                    return View(user);
                }

                if (user.Photo != null && user.Photo != "DefaultUser.jpg")
                {
                    _imageHelper.DeleteImage(user.Photo, "UserImages");
                }

                // Kullanıcıyı sil
                var result = await _service.UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return View();
                }

                await _service.SignInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in AppUserController.DeleteMyAccount (POST)");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }
    }
}
