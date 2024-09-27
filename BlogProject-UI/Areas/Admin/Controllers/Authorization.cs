using BlogProject.CORE.CoreModels.Models;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace BlogProject_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Authorization : Controller
    {
        private readonly IUnitOfWorkService _service;

        public Authorization(IUnitOfWorkService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
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
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "E-Posta adresiniz veya şifreniz yanlış.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E-Posta adresiniz veya şifreniz yanlış.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _service.SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { Area = "" });
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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Photo = "DefaultUser.jpg"
                };

                if (string.IsNullOrEmpty(user.Email))
                {
                    throw new ArgumentNullException(nameof(user.Email), "E-posta adresi boş olamaz");
                }

                var result = await _service.UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _service.UserManager.AddToRoleAsync(user, "Newuser");

                    var token = await _service.UserManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Authorization", new { userId = user.Id, token = token }, Request.Scheme);

                    await _service.EmailSender.SendEmailAsync(user.Email, "Email Doğrulama", $"Lütfen e-postanızı doğrulamak için şu linke tıklayın: {confirmationLink}");

                    await _service.SignInManager.SignInAsync(user, isPersistent: false);

                    TempData["Message"] = "Kayıt başarılı! Oturum açıldı ve doğrulama e-postası gönderildi.";
                    return RedirectToAction("Index", "Home", new { area = "Home" });
                }
            }

            ModelState.AddModelError("", "Kayıt başarısız. Lütfen bilgilerinizi kontrol edin.");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _service.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _service.UserManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                await _service.UserManager.RemoveFromRoleAsync(user, "Newuser");
                await _service.UserManager.AddToRoleAsync(user, "Verifieduser");

                await _service.SignInManager.RefreshSignInAsync(user);

                return RedirectToAction("ConfirmEmail", "AppUser", new { area = "Home" });
            }

            return View("Error");
        }


        [HttpPost]
        public async Task<IActionResult> ResendConfirmationEmail()
        {
            var user = await _service.UserManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var token = await _service.UserManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Authorization", new { userId = user.Id, token }, Request.Scheme);

            await _service.EmailSender.SendEmailAsync(user.Email, "Email Doğrulama", $"Lütfen e-postanızı doğrulamak için şu linke tıklayın: {confirmationLink}");

            ViewBag.IsConfirmationEmailSent = true;

            return RedirectToAction("UserSettings", "AppUser", new { area = "Home" });
        }
    }
}