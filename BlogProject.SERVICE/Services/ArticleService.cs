using AutoMapper;
using BlogProject.CORE.CoreModels.Enums;
using BlogProject.CORE.CoreModels.Models;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.IRepositories;
using BlogProject.SERVICE.Services.IServices;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CountAsync()
        {
            if (_unitOfWork.ArticleRepo == null)
            {
                return 0;
            }
            return await _unitOfWork.ArticleRepo.CountAsync();
        }

        public async Task<int> CountByCategoryId(string categoryId)
        {
            return await _unitOfWork.ArticleRepo.CountAsync(a => a.CategoryId == categoryId);
        }

        public int CreateArticle(ArticleCreateDTO articleCreateDTO)
        {
            var article = _mapper.Map<Article>(articleCreateDTO);
            return _unitOfWork.ArticleRepo.Add(article);
        }

        public async Task<int> CreateArticleAsync(Article article)
        {
            return await _unitOfWork.ArticleRepo.AddAsync(article);
        }

        public async Task<int> DeleteArticleAsync(string id)
        {
            var article = await _unitOfWork.ArticleRepo.GetByIdAsync(id);
            if (article is not null)
            {
                article.DeleteDate = DateTime.Now;
                article.Status = CORE.CoreModels.Enums.EntityStatus.Deleted;
                return await _unitOfWork.ArticleRepo.UpdateAsync(article);
            }
            return 0;
        }

        public async Task<IEnumerable<ArticleDTO>> GetAllActiveArticlesAsync()
        {
            var articles = await _unitOfWork.ArticleRepo.GetAllAsync(x => x.Status != CORE.CoreModels.Enums.EntityStatus.Deleted);
            return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
        }

        public async Task<IEnumerable<ArticleDTO>> GetAllArticlesAsync()
        {
            var articles = await _unitOfWork.ArticleRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
        }

        public async Task<IEnumerable<ArticleDTO>> GetAllArticlesByCategoryIdAsync(string categoryId)
        {
            return await _unitOfWork.ArticleRepo.GetFilteredModelListAysnc(
                select: x => new ArticleDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Thumbnail = x.Thumbnail,
                    CreateDate = x.CreateDate,
                    UpdateDate = x.UpdateDate,
                    AppUserId = x.AppUserId,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    UserName = x.AppUser.UserName,
                    Status = x.Status
                },
                where: x => x.CategoryId == categoryId && x.Status != EntityStatus.Deleted,
                orderBy: x => x.OrderBy(x => x.CreateDate)
            );
        }

        public async Task<IEnumerable<ArticleWithUserDTO>> GetAllArticlesByUserIdAsync(string userId)
        {
            return await _unitOfWork.ArticleRepo.GetFilteredModelListAysnc(
                select: x => new ArticleWithUserDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Thumbnail = x.Thumbnail,
                    CreateDate = x.CreateDate,
                    UpdateDate = x.UpdateDate,
                    AppUserId = x.AppUserId,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    UserName = x.AppUser.UserName,
                    Status = x.Status,
                    ViewsCount = x.ViewsCount,
                    CommentCount = x.Comments.Count
                },
                where: x => x.Status != EntityStatus.Deleted && x.AppUserId == userId,
                join: x => x.Include(x => x.AppUser)
                    .Include(x => x.Category)
                    .Include(x => x.Comments)
                );
        }

        public async Task<List<ArticleWithUserDTO>> GetAllArticlesWithUserAsync()
        {
            var articles = await _unitOfWork.ArticleRepo.GetAllWithIncludesAsync();
            return articles.Select(article => new ArticleWithUserDTO
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                Thumbnail = article.Thumbnail,
                CreateDate = article.CreateDate,
                UpdateDate = article.UpdateDate,
                AppUserId = article.AppUserId,
                CategoryId = article.CategoryId,
                CategoryName = article.Category.Name,
                UserName = article.AppUser.UserName,
                Status = article.Status,
                ViewsCount = article.ViewsCount,
                CommentCount = article.Comments.Count
            }).ToList();
        }

        public async Task<ArticleDTO> GetArticleByIdAsync(string id)
        {
            //var article = await _unitOfWork.ArticleRepo.GetByIdAsync(id);
            //return _mapper.Map<ArticleDTO>(article);
            var articles = await _unitOfWork.ArticleRepo.GetAllAsync();
            var article = articles.FirstOrDefault(x => x.Id == id);

            return _mapper.Map<ArticleDTO>(article);

        }

        public async Task<List<ArticleDTO>> GetArticlesForHomePageAsync()
        {
            var articles = await _unitOfWork.ArticleRepo.GetFilteredModelListAysnc(
                select: article => new ArticleDTO
                {
                    Id = article.Id,
                    Title = article.Title,
                    Content = article.Content.Length > 100 ? article.Content.Substring(0, 100) + "..." : article.Content,
                    Thumbnail = article.Thumbnail ?? "default-thumbnail.jpg",
                    CreateDate = article.CreateDate,
                    UserName = article.AppUser.UserName,
                    CategoryName = article.Category.Name
                },
                where: article => article.Status != EntityStatus.Deleted,
                join: article => article.Include(a => a.AppUser).Include(a => a.Category));
            return articles.ToList();
        }

        public async Task<List<Article>> GetArticlesWithCategoryAndUserAsync()
        {
            return await _unitOfWork.ArticleRepo.GetArticlesWithCategoryAndUserAsync();
        }

        public async Task<int> UpdateArticleAsync(ArticleDTO articleDTO)
        {
            var article = await _unitOfWork.ArticleRepo.GetByIdAsync(articleDTO.Id);

            _mapper.Map(articleDTO, article);
            article.UpdateDate = DateTime.Now;
            article.Status = EntityStatus.Updated;
            return await _unitOfWork.ArticleRepo.UpdateAsync(article);
        }

        public async Task<ArticleDetailDTO> GetArticleDetailByIdAsync(string id)
        {
            var article = await _unitOfWork.ArticleRepo.GetFilteredModelAysnc(
                select: x => new ArticleDetailDTO
                {
                    ArticleId = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Thumbnail = x.Thumbnail ?? "default-thumbnail.jpg",
                    CreateDate = x.CreateDate,
                    CategoryName = x.Category.Name,
                    UserName = x.AppUser.UserName,
                    UserProfileImage = x.AppUser.Photo ?? "default-user.jpg",
                    ViewCount = x.ViewsCount,
                    CommentCount = x.Comments.Count
                },
                where: x => x.Id == id,
                join: x => x.Include(x => x.Category).Include(x => x.AppUser).Include(x => x.Comments)
            );

            return article;
        }

    }
}