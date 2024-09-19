using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject_UI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly IUnitOfWorkService _service;

        public CommentController(IUnitOfWorkService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var comments = await _service.CommentService.GetCommentsWithArticleAndUserAsync();
            return View(comments);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }

    }
}
