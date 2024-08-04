using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Contexts;
using BlogProject.REPO.Repositories.BaseRepos;
using BlogProject.SERVICE.IRepositories;
using BlogProject.SERVICE.IRepositories.BaseRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Repositories
{
    public class RoleRepo : BaseRepo<Role>, IRoleRepo
    {
        public RoleRepo(AppDbContext context) : base(context)
        {
        }
    }
}
