using BlogProject.CORE.CoreModels.Models;
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
        int CreateArticle(ArticleCreateDTO articleCreateDTO);
        Task<int> CreateArticleAsync(Article article);
        Task<int> UpdateArticleAsync(ArticleDTO articleDTO);
        Task<int> DeleteArticleAsync(string id);
        Task<int> CountAsync();
        Task<int> CountByCategoryId(string categoryId);
        Task<List<ArticleWithUserDTO>> GetAllArticlesWithUserAsync();
        Task<List<Article>> GetArticlesWithCategoryAndUserAsync();
        Task<List<ArticleDTO>> GetArticlesForHomePageAsync(string categoryId = null);
        Task<List<ArticleDTO>> GetArticlesByUserIdAsync(string userId);
        Task<ArticleDetailDTO> GetArticleDetailByIdAsync(string id);
    }
}
