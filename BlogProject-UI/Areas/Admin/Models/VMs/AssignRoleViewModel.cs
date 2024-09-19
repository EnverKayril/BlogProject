using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogProject_UI.Areas.Admin.Models.VMs
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; } 
        public SelectList Roles { get; set; }
        public string SelectedRole { get; set; }
    }
}
