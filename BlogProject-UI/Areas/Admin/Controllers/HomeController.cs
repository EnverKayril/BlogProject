﻿using BlogProject.REPO.Utilities.UnitOfWork;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using BlogProject_UI.Areas.Admin.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogProject_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Editor")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWorkService _service;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWorkService service, IUnitOfWork unitOfWork)
        {
            _service = service;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = new DashboardViewModel
                {
                    CategoriesCount = await _unitOfWork.CategoryRepo.CountAsync(),
                    ArticlesCount = await _unitOfWork.ArticleRepo.CountAsync(),
                    CommentsCount = await _unitOfWork.CommentRepo.CountAsync(),
                    UsersCount = await _unitOfWork.AppUserRepo.CountAsync(),
                    Articles = (await _unitOfWork.ArticleRepo.GetArticlesWithCategoryAndUserAsync()).ToList()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex, "Error occurred in HomeController.Index");
                return RedirectToAction("HandleStatusCode", "Error", new { statusCode = 500 });
            }
        }
    }
}
