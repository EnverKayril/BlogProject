using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Areas.Admin.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace BlogProject_UI.Areas.Admin.ViewComponents
{
    public class UserMenuViewComponent : ViewComponent
    {
        private readonly IUnitOfWorkService _service;

        public UserMenuViewComponent(IUnitOfWorkService service)
        {
            _service = service;
        }

        public ViewViewComponentResult Invoke()
        {
            var user = _service.UserManager.GetUserAsync(HttpContext.User).Result;
            return View(new UserViewModel
            {
                User = user
            });
        }
    }
}
