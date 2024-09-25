using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Models.VMs;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.ViewComponents
{
    public class UserSettingsSidebarViewComponent : ViewComponent
    {
        private readonly IUnitOfWorkService _service;

        public UserSettingsSidebarViewComponent(IUnitOfWorkService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _service.UserManager.GetUserAsync(HttpContext.User);

            var model = new UserSettingsSidebarViewModel
            {
                UserName = user.UserName,
                Photo = user.Photo
            };

            return View(model);
        }
    }
}
