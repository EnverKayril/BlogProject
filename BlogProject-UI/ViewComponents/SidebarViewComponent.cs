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
			// Kategoriler
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
            var randomCategory = new Random();
            var randomCategories = categoryViewModels
                .OrderBy(c => randomCategory.Next())
                .Take(6)
                .ToList();

			// Makaleler
			var articles = await _service.ArticleService.GetAllArticlesAsync();
			var articleViewModel = new List<MostCommentedArticleViewModel>();
			foreach (var article in articles)
			{
				var commentCount = await _service.CommentService.CountByArticleId(article.Id);

				articleViewModel.Add(new MostCommentedArticleViewModel
				{
					ArticleId = article.Id,
					ArticleTitle = article.Title,
					CommentCount = commentCount
				});
			}
			var mostCommentedArticles = articleViewModel
				.OrderByDescending(a => a.CommentCount)
				.Take(6)
				.ToList();

			// Kullanıcılar
			var users = await _service.AppUserService.GetAllAppUserAsync();
			var appUserViewModel = new List<RandomAppUserViewModel>();
			foreach (var appUser in users)
			{
				appUserViewModel.Add(new RandomAppUserViewModel
				{
					AppUserId = appUser.Id,
					AppUserName = appUser.UserName,
					ProfilePicture = appUser.Photo
				});
			}
			var randomAppUser = new Random();
			var randomAppUsers = appUserViewModel
				.OrderBy (c => randomAppUser.Next())
				.Take(6)
				.ToList();

			// Yorumlar
			var comments = await _service.CommentService.GetAllCommentsAsync();

			var recentComments = comments
				.OrderByDescending(c => c.CreateDate)
				.Take(6)
				.Select(c => new RecentCommentViewModel
				{
					CommentId = c.Id,
					CommentContent = c.Content,
					ArticleId = c.ArticleId,
					CommentDate = c.CreateDate
				})
				.ToList();

			var sidebarViewModel = new SidebarViewModel
			{
				Categories = randomCategories,
				MostCommentedArticles = mostCommentedArticles,
				RandomAppUsers = randomAppUsers,
				RecentComments = recentComments
			};

			return View(sidebarViewModel);
		}
	}
}
