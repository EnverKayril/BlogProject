using BlogProject.SERVICE.DTOs;

namespace BlogProject_UI.Models.VMs
{
    public class ArticleDetailViewModel
    {
        public ArticleDTO Article { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }
        public CommentCreateDTO NewComment { get; set; }
    }
}
