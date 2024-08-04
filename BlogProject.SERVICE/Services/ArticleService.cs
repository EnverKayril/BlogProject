using AutoMapper;
using BlogProject.CORE.CoreModels.Models;
using BlogProject.SERVICE.DTOs;
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

        public int CreateArticle(ArticleDTO articleDTO)
        {
            var article = _mapper.Map<Article>(articleDTO);
            return _unitOfWork.ArticleRepo.Add(article);
        }

        public async Task<int> DeleteArticle(string id)
        {
            var article = await _unitOfWork.ArticleRepo.GetByIdAsync(id);
            if (article is not null)
            {
                article.DeleteDate = DateTime.Now;
                article.Status = CORE.CoreModels.Enums.EntityStatus.Deleted;
                return _unitOfWork.ArticleRepo.Delete(article);
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
                    CreateDate = x.CreateDate,
                    Content = x.Content,
                    Thumbnail = x.Thumbnail,
                    CategoryId = x.CategoryId,
                    AppUserId = x.AppUserId
                },
                where: x => x.CategoryId == categoryId,
                orderBy: x => x.OrderBy(x => x.Content));
        }

        public async Task<IEnumerable<ArticleWithUserDTO>> GetAllArticlesByUserIdAsync(string userId)
        {
            return await _unitOfWork.ArticleRepo.GetFilteredModelListAysnc(
                select: x => new ArticleWithUserDTO
                {
                    Id = x.Id,
                    Title = x.Title,
                    CreateDate = x.CreateDate,
                    Content = x.Content,
                    Thumbnail = x.Thumbnail,
                    CategoryId = x.CategoryId,
                    AppUserId = x.AppUserId,
                    FirstName = x.AppUser.FirstName,
                    LastName = x.AppUser.LastName
                },
                where: x => x.Status != CORE.CoreModels.Enums.EntityStatus.Deleted & x.AppUserId == userId,
                join: x => x.Include(x => x.AppUser)
                );
        }

        public async Task<ArticleDTO> GetArticleByIdAsync(string id)
        {
            var article = await _unitOfWork.ArticleRepo.GetByIdAsync(id);
            return _mapper.Map<ArticleDTO>(article);
        }

        public int UpdateArticle(ArticleDTO articleDTO)
        {
            var article = _mapper.Map<Article>(articleDTO);
            article.UpdateDate = DateTime.Now;
            article.Status = CORE.CoreModels.Enums.EntityStatus.Updated;
            return _unitOfWork.ArticleRepo.Update(article);
        }
    }
}