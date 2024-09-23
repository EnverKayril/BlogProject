using BlogProject.CORE.CoreModels.Models;
using BlogProject.SERVICE.DTOs;

namespace BlogProject_UI.Models.VMs
{
    public class CategoryDetailViewModel
    {
        public CategoryDTO Category { get; set; }
        public IEnumerable<ArticleDTO> Articles { get; set; }
    }
}
