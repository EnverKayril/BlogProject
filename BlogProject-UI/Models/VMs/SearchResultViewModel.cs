using BlogProject.SERVICE.DTOs;

namespace BlogProject_UI.Models.VMs
{
	public class SearchResultViewModel
	{
		public List<ArticleDTO> Articles { get; set; }
		public List<CategoryDTO> Categories { get; set; }
		public List<AppUserDTO> Users { get; set; }
	}
}
