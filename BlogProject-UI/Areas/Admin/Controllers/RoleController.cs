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

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _service.RoleManager.Roles.ToListAsync();
            return View(roles);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpGet]
        public async Task<IActionResult> AssignRole(string userId)
        {
            var user = await _service.UserManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var roles = await _service.RoleManager.Roles.ToListAsync();
            var userroles = await _service.UserManager.GetRolesAsync(user);

            var model = new AssignRoleViewModel
            {
                UserId = userId,
                Roles = new SelectList(roles, "Name", "Name", userroles.FirstOrDefault()),
                SelectedRole = userroles.FirstOrDefault()
            };
            return View(model);
        }

        [Authorize(Roles = "Admin, Editor")]
        [HttpPost]
        public async Task<IActionResult> AssignRole(AssignRoleViewModel model)
        {
            var user = await _service.UserManager.FindByIdAsync(model.UserId);

            var userRoles = await _service.UserManager.GetRolesAsync(user);

            var removeRolesResult = await _service.UserManager.RemoveFromRolesAsync(user, userRoles);

            var addRoleResult = await _service.UserManager.AddToRoleAsync(user, model.SelectedRole);

            return RedirectToAction("Index", "AppUser");
        }


    }
}
