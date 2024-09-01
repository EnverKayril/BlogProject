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
        IArticleRepo ArticleRepo { get; }
        ICategoryRepo CategoryRepo { get; }
        ICommentRepo CommentRepo { get; }
        IAppUserRepo AppUserRepo { get; }

    }
}
