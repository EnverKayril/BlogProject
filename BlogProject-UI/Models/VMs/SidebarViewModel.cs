namespace BlogProject_UI.Models.VMs
{
    public class SidebarViewModel
    {
        public List<CategoryWithArticleCountViewModel> Categories { get; set; }
    }

    public class CategoryWithArticleCountViewModel
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ArticleCount { get; set; }
    }
}
