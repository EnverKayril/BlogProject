using AutoMapper;
using BlogProject.CORE.CoreModels.Models;
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

        public UnitOfWorkService(IUnitOfWork unitOfWorkService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWorkService;
            _mapper = mapper;
            _userManager = userManager;

            CategoryService = new CategoryService(_unitOfWork, _mapper);
            AppUserService = new AppUserService(_unitOfWork, _mapper, _userManager);
        }

        public ICategoryService CategoryService { get; }

        public IArticleService ArticleService {  get; }

        public IAppUserService AppUserService { get; }

        public UserManager<AppUser> UserManager => _userManager;
    }
}
