using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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


    }
}
