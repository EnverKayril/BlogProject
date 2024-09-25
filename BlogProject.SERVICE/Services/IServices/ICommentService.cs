using BlogProject.CORE.CoreModels.Models;
using BlogProject.SERVICE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Services.IServices
{
    public interface ICommentService
    {
        Task<CommentDTO> GetCommentByIdAsync(string id);
        Task<CommentWithUserDTO> GetCommentByIdWithArticleAndUserAsync(string id);
        Task<IEnumerable<CommentDTO>> GetAllCommentsAsync();
        Task<IEnumerable<CommentDTO>> GetAllActiveCommentsAsync();
        Task<IEnumerable<CommentWithUserDTO>> GetAllCommentsByUserIdAsync(string userId);
        Task<IEnumerable<CommentDTO>> GetAllCommentsByArticleIdAsync(string articleId);
        int CreateComment(CommentCreateDTO commentCreateDTO);
        Task<int> CreateCommentAsync(CommentCreateDTO commentCreateDTO);
        Task<int> UpdateCommentAsync(CommentDTO commentDTO);
        Task<int> DeleteCommentAsync(string id);
        Task<int> CountAsync();
        Task<int> CountByArticleId(string id);
        Task<List<CommentWithUserDTO>> GetCommentsWithArticleAndUserAsync();
        Task<List<CommentWithUserDTO>> GetCommentsWithArticleAndUserAsync(bool onlyUnapproved);
    }
}
