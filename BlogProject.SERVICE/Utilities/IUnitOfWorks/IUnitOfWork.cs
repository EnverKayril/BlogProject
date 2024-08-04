using BlogProject.SERVICE.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Utilities.IUnitOfWorks
{
    public interface IUnitOfWork
    {
        IAppUserRepo AppUserRepo { get; }
        IArticleRepo ArticleRepo { get; }
        ICategoryRepo CategoryRepo { get; }
        ICommentRepo CommentRepo { get; }
        IRoleRepo RoleRepo { get; }
    }
}
