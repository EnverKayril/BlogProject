using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Areas.Admin.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BlogProject_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IUnitOfWorkService _service;

        public RoleController(IUnitOfWorkService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var roles = await _service.RoleManager.Roles.ToListAsync();
                return View(roles);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in RoleController.Index");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AssignRole(string userId)
        {
            try
            {
                var user = await _service.UserManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }
                var roles = await _service.RoleManager.Roles.ToListAsync();
                var userRoles = await _service.UserManager.GetRolesAsync(user);
                var model = new AssignRoleViewModel
                {
                    UserId = userId,
                    Roles = new SelectList(roles, "Name", "Name", userRoles.FirstOrDefault()),
                    SelectedRole = userRoles.FirstOrDefault()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in RoleController.AssignRole [GET]");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
        {
            try
            {
                var user = await _service.UserManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 404 });
                }

                var userRoles = await _service.UserManager.GetRolesAsync(user);

                var removeRolesResult = await _service.UserManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeRolesResult.Succeeded)
                {
                    return View(model);
                }

                var addRoleResult = await _service.UserManager.AddToRoleAsync(user, model.SelectedRole);
                if (!addRoleResult.Succeeded)
                {
                    return View(model);
                }
                return RedirectToAction("Index", "AppUser");
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in RoleController.AssignRole");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }
    }
}