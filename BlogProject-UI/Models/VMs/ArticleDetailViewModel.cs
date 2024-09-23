using BlogProject.SERVICE.DTOs;

namespace BlogProject_UI.Models.VMs
{
    public class ArticleDetailViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserProfilePicture { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CommentCount { get; set; }
        public int ViewsCount { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}