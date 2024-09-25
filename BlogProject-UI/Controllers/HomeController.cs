using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Models.VMs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace BlogProject_UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUnitOfWorkService _service;

		public HomeController(IUnitOfWorkService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index(int p = 1)
		{
            var articles = (await _service.ArticleService.GetArticlesForHomePageAsync()).OrderByDescending(a => a.CreateDate);

            var pagedarticles = articles.ToPagedList(p, 5);
			return View(pagedarticles);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult Terms()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View();
		}

        [HttpGet]
        public async Task<IActionResult> SearchResults(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return View(new SearchResultViewModel());
            }

            var articles = await _service.ArticleService.GetAllArticlesAsync();
            var filteredArticles = articles
                .Where(a => a.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                            || a.Content.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var categories = await _service.CategoryService.GetAllCategoriesAsync();
            var filteredCategories = categories
                .Where(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var users = await _service.AppUserService.GetAllAppUserAsync();
            var filteredUsers = users
                .Where(u => u.UserName.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var model = new SearchResultViewModel
            {
                Articles = filteredArticles,
                Categories = filteredCategories,
                Users = filteredUsers
            };

            ViewBag.Query = query;

            return View(model);
        }
    }
}
