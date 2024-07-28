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
    public class CommentRepo : BaseRepo<Comment>, ICommentRepo
    {
        private readonly AppDbContext _appDbContext;
        public CommentRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByArticleId(string articleId)
        {
            return await _appDbContext.Comments.Where(c => c.ArticleId == articleId).ToListAsync();
        }
    }
}
