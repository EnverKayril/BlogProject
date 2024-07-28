using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Contexts;
using BlogProject.REPO.Repositories.BaseRepos;
using BlogProject.SERVICE.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Repositories
{
    public class AppUserRepo : BaseRepo<AppUser>, IAppUserRepo
    {
        private readonly AppDbContext _context;
        public AppUserRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserByNameAsync(string userName)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
