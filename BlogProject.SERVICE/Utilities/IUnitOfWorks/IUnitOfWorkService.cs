using BlogProject.SERVICE.Services.IServices;
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
    }
}
