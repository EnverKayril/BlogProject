namespace BlogProject_UI.Models.VMs
{
    public class SidebarViewModel
	{
		public List<CategoryWithArticleCountViewModel> Categories { get; set; }
		public List<MostCommentedArticleViewModel> MostCommentedArticles { get; set; }
		public List<RandomAppUserViewModel> RandomAppUsers{ get; set; }
		public List<RecentCommentViewModel> RecentComments { get; set; }
	}

	public class CategoryWithArticleCountViewModel
	{
		public string CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int ArticleCount { get; set; }
	}

	public class MostCommentedArticleViewModel
	{
		public string ArticleId { get; set; }
		public string ArticleTitle { get; set; }
		public int CommentCount { get; set; }
		public string ShortTitle
		{
			get
			{
				return ArticleTitle.Length > 30 ? ArticleTitle.Substring(0, 30) + "..." : ArticleTitle;
			}
		}
	}

	public class RandomAppUserViewModel
	{
		public string AppUserId { get; set; }
		public string AppUserName { get; set; }
		public string ProfilePicture { get; set; }
	}

	public class RecentCommentViewModel
	{
		public string CommentId { get; set; }
		public string CommentContent { get; set; }
		public string ArticleId { get; set; } 
		public DateTime CommentDate { get; set; }

		public string ShortContent
		{
			get
			{
				return CommentContent.Length > 40 ? CommentContent.Substring(0, 40) + "..." : CommentContent;
			}
		}
	}


}
