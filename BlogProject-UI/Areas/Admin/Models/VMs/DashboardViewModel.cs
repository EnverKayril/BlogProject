using BlogProject.CORE.CoreModels.Models;
using BlogProject.SERVICE.DTOs;

namespace BlogProject_UI.Areas.Admin.Models.VMs
{
    public class DashboardViewModel
    {
        public int CategoriesCount { get; set; }
        public int ArticlesCount { get; set; }
        public int CommentsCount { get; set; }
        public int UsersCount { get; set; }
        public IList<Article> Articles { get; set; }
    }
}
