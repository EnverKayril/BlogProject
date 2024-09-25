using BlogProject.SERVICE.DTOs;

namespace BlogProject_UI.Models.VMs
{
    public class UserWithArticlesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhoto { get; set; }
        public IEnumerable<ArticleWithUserDTO> Articles { get; set; }
    }
}
