using BlogProject.CORE.CoreModels.Models;

namespace BlogProject_UI.Areas.Admin.Models.VMs
{
    public class UserWithRolesViewModel
    {
        public AppUser AppUser { get; set; }
        public IList<string> Roles { get; set; }
    }
}
