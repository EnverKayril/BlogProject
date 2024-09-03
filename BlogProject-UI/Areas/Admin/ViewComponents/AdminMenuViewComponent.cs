using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Areas.Admin.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace BlogProject_UI.Areas.Admin.ViewComponents
{
    public class AdminMenuViewComponent : ViewComponent
    {
        private readonly IUnitOfWorkService _service;

        public AdminMenuViewComponent(IUnitOfWorkService service)
        {
            _service = service;
        }

        public ViewViewComponentResult Invoke()
        {
            var user = _service.UserManager.GetUserAsync(HttpContext.User).Result;
            var roles = _service.UserManager.GetRolesAsync(user).Result;
            return View(new UserWithRolesViewModel
            {
                AppUser = user,
                Roles = roles
            });
        }
    }
}
