using BlogProject.CORE.CoreModels.Models;
using BlogProject.REPO.Contexts;
using BlogProject.REPO.Repositories.BaseRepos;
using BlogProject.SERVICE.IRepositories;
using BlogProject.SERVICE.IRepositories.BaseRepos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Repositories
{
    public class ArticleRepo : BaseRepo<Article>, IArticleRepo
    {
        private readonly AppDbContext _context;
        public ArticleRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(Expression<Func<Article, bool>> predicate)
        {
            return await _context.Articles.CountAsync(predicate);
        }

        public async Task<List<Article>> GetAllWithIncludesAsync()
        {
            return await _context.Articles
            .Include(a => a.Category)
            .Include(a => a.AppUser)
            .Include(a => a.Comments)
            .ToListAsync();
        }

        public async Task<List<Article>> GetArticlesWithCategoryAndUserAsync()
        {
            return await _context.Articles
                             .Include(a => a.Category)
                             .Include(a => a.AppUser)
                             .ToListAsync();
        }
    }
}
