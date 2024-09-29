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
            if (!ModelState.IsValid)
            {
                NLog.LogManager.GetCurrentClassLogger().Warn("Geçersiz giriş denemesi: ModelState is not valid.");
                return View();
            }

            try
            {
                var user = await _service.UserManager.FindByEmailAsync(userLoginDTO.Email);

                if (user != null)
                {
                    var result = await _service.SignInManager.PasswordSignInAsync(user, userLoginDTO.Password, userLoginDTO.RememberMe, false);

                    if (result.Succeeded)
                    {
                        NLog.LogManager.GetCurrentClassLogger().Info($"Kullanıcı giriş yaptı: {user.Email}");
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    else
                    {
                        NLog.LogManager.GetCurrentClassLogger().Warn($"Başarısız giriş denemesi: {user.Email}");

                        return View();
                    }
                }
                else
                {
                    NLog.LogManager.GetCurrentClassLogger().Warn($"Kullanıcı bulunamadı: {userLoginDTO.Email}");
                    return View();
                }
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Login işlemi sırasında bir hata oluştu.");
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _service.SignInManager.SignOutAsync();
                NLog.LogManager.GetCurrentClassLogger().Info($"Kullanıcı çıkış yaptı: {User.Identity.Name}");
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Logout işlemi sırasında bir hata oluştu.");
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
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
                try
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

                            NLog.LogManager.GetCurrentClassLogger().Info($"Kullanıcı şifresini değiştirdi: {user.UserName}");

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            NLog.LogManager.GetCurrentClassLogger().Warn($"Kullanıcı şifre değiştirme hatası: {user.UserName}, Hatalar: {string.Join(", ", result.Errors.Select(e => e.Description))}");

                            return View(userPasswordChanceDTO);
                        }
                    }
                    else
                    {
                        NLog.LogManager.GetCurrentClassLogger().Warn($"Kullanıcı yanlış şifre girdi: {user.UserName}");
                        return View(userPasswordChanceDTO);
                    }
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex, "Şifre değiştirme işlemi sırasında bir hata oluştu.");
                    return View(userPasswordChanceDTO);
                }
            }
            else
            {
                return View(userPasswordChanceDTO);
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
                try
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

                        NLog.LogManager.GetCurrentClassLogger().Info($"Kullanıcı kaydedildi ve doğrulama e-postası gönderildi: {user.Email}");

                        await _service.SignInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction("Index", "Home", new { area = "Home" });
                    }

                    NLog.LogManager.GetCurrentClassLogger().Warn($"Kullanıcı kaydı başarısız: {user.Email}");
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex, "Kayıt işlemi sırasında bir hata oluştu.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                NLog.LogManager.GetCurrentClassLogger().Warn("E-posta doğrulama sırasında userId veya token null geldi.");
                return RedirectToAction("Index", "Home");
            }

            var user = await _service.UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                NLog.LogManager.GetCurrentClassLogger().Warn($"E-posta doğrulama sırasında kullanıcı bulunamadı. userId: {userId}");
                return NotFound();
            }

            var result = await _service.UserManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                NLog.LogManager.GetCurrentClassLogger().Info($"Kullanıcı e-posta doğrulandı. userId: {user.Id}");

                await _service.UserManager.RemoveFromRoleAsync(user, "Newuser");
                await _service.UserManager.AddToRoleAsync(user, "Verifieduser");
                await _service.SignInManager.RefreshSignInAsync(user);
                return RedirectToAction("ConfirmEmail", "AppUser", new { area = "Home" });
            }
            NLog.LogManager.GetCurrentClassLogger().Warn($"E-posta doğrulama başarısız oldu. userId: {user.Id}");
            return View("Error");
        }

        [Authorize]
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> ResendConfirmationEmail()
        {
            var user = await _service.UserManager.GetUserAsync(User);
            if (user == null)
            {
                NLog.LogManager.GetCurrentClassLogger().Warn("Kullanıcı oturum açmamışken doğrulama e-postası yeniden gönderilmeye çalışıldı.");
                return RedirectToAction("Login", "Account");
            }

            var token = await _service.UserManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Authorization", new { userId = user.Id, token }, Request.Scheme);

            try
            {
                await _service.EmailSender.SendEmailAsync(user.Email, "Email Doğrulama", $"Lütfen e-postanızı doğrulamak için şu linke tıklayın: {confirmationLink}");
                NLog.LogManager.GetCurrentClassLogger().Info($"Doğrulama e-postası yeniden gönderildi. userId: {user.Id}");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, $"Doğrulama e-postası gönderimi başarısız oldu. userId: {user.Id}");
                return View("UserSettings");
            }
            ViewBag.IsConfirmationEmailSent = true;
            return RedirectToAction("UserSettings", "AppUser", new { area = "Home" });
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _service.UserManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        var token = await _service.UserManager.GeneratePasswordResetTokenAsync(user);
                        var resetLink = Url.Action("ResetPassword", "Authorization", new { token = token, email = model.Email }, Request.Scheme);

                        await _service.EmailSender.SendEmailAsync(model.Email, "Şifre Sıfırlama", $"Şifrenizi sıfırlamak için <a href='{resetLink}'>buraya tıklayın</a>.");

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return RedirectToAction("ForgotPassword");
                    }
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex, "ForgotPassword işleminde hata oluştu.");
                    return RedirectToAction("ForgotPassword");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                return RedirectToAction("Error", "Home", new { statusCode = 400 });
            }

            var model = new ResetPasswordDTO { Token = token, Email = email };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _service.UserManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var resetPassResult = await _service.UserManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (resetPassResult.Succeeded)
                {
                    return RedirectToAction("Login");
                }

                return RedirectToAction("Error", "Home", new { statusCode = 500 });
            }

            return RedirectToAction("Error", "Home", new { statusCode = 404 });
        }
    }
}