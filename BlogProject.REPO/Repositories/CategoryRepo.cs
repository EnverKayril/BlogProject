using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Contexts;
using BlogProject.REPO.Repositories.BaseRepos;
using BlogProject.SERVICE.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Repositories
{
    public class CategoryRepo : BaseRepo<Category>, ICategoryRepo
    {
        public CategoryRepo(AppDbContext context) : base(context)
        {
        }
    }
}
