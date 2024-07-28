using BlogProject.CORE.CoreModels.Models;
using BlogProject.SERVICE.IRepositories.BaseRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.IRepositories
{
    public interface IArticleRepo : IBaseRepo<Article>
    {
    }
}
