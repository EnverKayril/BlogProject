﻿using BlogProject.CORE.CoreModels.Models;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.IRepositories.BaseRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.IRepositories
{
    public interface ICommentRepo : IBaseRepo<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByArticleId(string articleId);
        Task<List<CommentWithUserDTO>> GetCommentsWithArticleAndUserAsync();
        Task<int> CountAsync(Expression<Func<Comment, bool>> predicate);
    }
}
