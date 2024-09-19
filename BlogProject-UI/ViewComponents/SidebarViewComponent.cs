using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Models.VMs;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly IUnitOfWorkService _service;

        public SidebarViewComponent(IUnitOfWorkService service)
        {
            _service = service;
        }

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = await _service.CategoryService.GetAllCategoriesAsync();

			var categoryViewModels = new List<CategoryWithArticleCountViewModel>();

			foreach (var category in categories)
			{
				var articleCount = await _service.ArticleService.CountByCategoryId(category.Id);

				categoryViewModels.Add(new CategoryWithArticleCountViewModel
				{
					CategoryId = category.Id,
					CategoryName = category.Name,
					ArticleCount = articleCount
				});
			}

            var random = new Random();
            var randomCategories = categoryViewModels
                .OrderBy(c => random.Next())
                .Take(6)
                .ToList();

            var sidebarViewModel = new SidebarViewModel
			{
                Categories = randomCategories
            };

			return View(sidebarViewModel);
		}
	}
}
