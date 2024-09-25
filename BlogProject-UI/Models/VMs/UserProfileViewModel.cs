using BlogProject.SERVICE.DTOs;

namespace BlogProject_UI.Models.VMs
{
    public class UserProfileViewModel
    {
        public AppUserDTO AppUserDTO { get; set; }
        public UserPasswordChanceDTO UserPasswordChangeDTO { get; set; }
    }
}
