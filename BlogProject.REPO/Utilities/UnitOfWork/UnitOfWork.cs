using BlogProject.REPO.Contexts;
using BlogProject.REPO.Repositories;
using BlogProject.SERVICE.IRepositories;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Utilities.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            AppUserRepo = new AppUserRepo(context);
            ArticleRepo = new ArticleRepo(context);
            CategoryRepo = new CategoryRepo(context);
            CommentRepo = new CommentRepo(context);

        }
        public IAppUserRepo AppUserRepo { get; }

        public IArticleRepo ArticleRepo { get; }

        public ICategoryRepo CategoryRepo { get; }

        public ICommentRepo CommentRepo { get; }
    }
}
