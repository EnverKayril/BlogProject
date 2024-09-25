using BlogProject.SERVICE.DTOs;

namespace BlogProject_UI.Models.VMs
{
    public class UserSettingsSidebarViewModel
    {
        public string UserName { get; set; }
        public string Photo { get; set; }
        public List<ArticleWithUserDTO> Articles { get; set; }
        public List<CommentWithUserDTO> Comments { get; set; }
    }
}
