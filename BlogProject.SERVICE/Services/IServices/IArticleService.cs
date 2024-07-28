using BlogProject.SERVICE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Services.IServices
{
    public interface IArticleService
    {
        Task<ArticleDTO> GetArticleByIdAsync(string id);
        Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync();
        Task<IEnumerable<ArticleDTO>> GetAllActiveArticlesAsync();
        Task<IEnumerable<ArticleWithUserDTO>> GetAllArticlesByUserIdAsync(string userId);
        Task<IEnumerable<ArticleDTO>> GetAllArticlesByCategoryIdAsync(string categoryId);
        int CreateArticle(ArticleDTO articleDTO);
        int UpdateArticle(ArticleDTO articleDTO);
        Task<int> DeleteArticle(string id);
    }
}
