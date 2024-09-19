using AutoMapper;
using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Utilities.Extensions;
using BlogProject.SERVICE.IRepositories;
using BlogProject.SERVICE.Services;
using BlogProject.SERVICE.Services.IServices;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Utilities.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ImageHelper _imageHelper;

        public UnitOfWorkService(IUnitOfWork unitOfWorkService, IMapper mapper, UserManager<AppUser> userManager, RoleManager<Role> roleManager, SignInManager<AppUser> signInManager, ImageHelper imageHelper)
        {
            _unitOfWork = unitOfWorkService;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _signInManager = signInManager;
            _imageHelper = imageHelper;

            CategoryService = new CategoryService(_unitOfWork, _mapper);
            AppUserService = new AppUserService(_unitOfWork, _mapper, _userManager);
            ArticleService = new ArticleService(_unitOfWork, _mapper);
            CommentService = new CommentService(_unitOfWork, _mapper);
        }

        public ICategoryService CategoryService { get; }

        public IArticleService ArticleService {  get; }

        public IAppUserService AppUserService { get; }

        public ICommentService CommentService { get; }
        public UserManager<AppUser> UserManager => _userManager;
        public SignInManager<AppUser> SignInManager => _signInManager;

        public RoleManager<Role> RoleManager => _roleManager;
    }
}
