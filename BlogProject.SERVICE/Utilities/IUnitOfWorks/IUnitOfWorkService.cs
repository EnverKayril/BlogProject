﻿using BlogProject.CORE.CoreModels.Models;
using BlogProject.SERVICE.IRepositories;
using BlogProject.SERVICE.Services.IServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Utilities.IUnitOfWorks
{
    public interface IUnitOfWorkService
    {
        ICategoryService CategoryService { get; }
        IArticleService ArticleService { get; }
        IAppUserService AppUserService { get; }
        UserManager<AppUser> UserManager { get; }

    }
}
