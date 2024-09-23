using AutoMapper;
using BlogProject.CORE.CoreModels.Enums;
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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CountAsync()
        {
            if (_unitOfWork.CommentRepo == null)
            {
                return 0;
            }
            return await _unitOfWork.CommentRepo.CountAsync();
        }

        public int CreateComment(CommentCreateDTO commentCreateDTO)
        {
            var comment = _mapper.Map<Comment>(commentCreateDTO);
            return _unitOfWork.CommentRepo.Add(comment);
        }

        public async Task<int> CreateCommentAsync(CommentCreateDTO commentCreateDTO)
        {
            var comment = _mapper.Map<Comment>(commentCreateDTO);
            return await _unitOfWork.CommentRepo.AddAsync(comment);
        }

        public async Task<int> DeleteCommentAsync(string id)
        {
            var comment = await _unitOfWork.CommentRepo.GetByIdAsync(id);
            if (comment != null)
            {
                comment.DeleteDate = DateTime.Now;
                comment.Status = CORE.CoreModels.Enums.EntityStatus.Deleted;
                return await _unitOfWork.CommentRepo.UpdateAsync(comment);
            }
            return 0;
        }

        public async Task<IEnumerable<CommentDTO>> GetAllActiveCommentsAsync()
        {
            var comment = await _unitOfWork.CommentRepo.GetAllAsync(x => x.Status != CORE.CoreModels.Enums.EntityStatus.Deleted);
            return _mapper.Map<IEnumerable<CommentDTO>>(comment);
        }

        public async Task<IEnumerable<CommentDTO>> GetAllCommentsAsync()
        {
            var comment = await _unitOfWork.CommentRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<CommentDTO>>(comment);
        }

        public async Task<IEnumerable<CommentDTO>> GetAllCommentsByArticleIdAsync(string articleId)
        {
            var comments = await _unitOfWork.CommentRepo.GetFilteredModelListAysnc(
                select: x => new CommentDTO
                {
                    Id = x.Id,
                    Content = x.Content,
                    AppUserId = x.AppUserId,
                    CreateDate = x.CreateDate,
                    UpdateDate = x.UpdateDate,
                    Status = x.Status
                },
                where: x => x.ArticleId == articleId && x.Status != CORE.CoreModels.Enums.EntityStatus.Deleted && x.Approved == true,
                orderBy: x => x.OrderBy(x => x.CreateDate));

            var userCache = new Dictionary<string, AppUser>();

            foreach (var comment in comments)
            {
                if (!userCache.ContainsKey(comment.AppUserId))
                {
                    var user = await _unitOfWork.AppUserRepo.GetByIdAsync(comment.AppUserId);
                    if (user != null)
                    {
                        userCache[comment.AppUserId] = user;
                    }
                }

                var cachedUser = userCache[comment.AppUserId];
                if (cachedUser != null)
                {
                    comment.UserName = cachedUser.UserName;
                    comment.UserPhoto = cachedUser.Photo;
                }
            }

            return comments;
        }

        public async Task<IEnumerable<CommentWithUserDTO>> GetAllCommentsByUserIdAsync(string userId)
        {
            return await _unitOfWork.CommentRepo.GetFilteredModelListAysnc(
                select: x => new CommentWithUserDTO
                {
                    CommentId = x.Id,
                    CommentContent = x.Content,
                    ArticleId = x.ArticleId,
                    UserName = x.AppUser.UserName,
                    CreateDate = x.CreateDate,
                    UpdateDate = x.UpdateDate,
                    Status = x.Status
                },
                where: x => x.Status != EntityStatus.Deleted && x.AppUserId == userId,
                join: x => x.Include(x => x.AppUser)
                .Include(x => x.Article)
            );
        }

        public async Task<CommentDTO> GetCommentByIdAsync(string id)
        {
            var comments = await _unitOfWork.CommentRepo.GetAllAsync();
            var comment = comments.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<List<CommentWithUserDTO>> GetCommentsWithArticleAndUserAsync()
        {
            var comments = await _unitOfWork.CommentRepo.GetCommentsWithArticleAndUserAsync();

            var commentDTO = comments.Select(comment => new CommentWithUserDTO
            {
                CommentId = comment.CommentId,
                CommentContent = comment.CommentContent,
                CreateDate = comment.CreateDate,
                UserName = comment.UserName,
                ArticleTitle = comment.ArticleTitle,
                Status = comment.Status,
                Approved = comment.Approved
            }).ToList();

            return commentDTO;
        }

        public async Task<int> UpdateCommentAsync(CommentDTO commentDTO)
        {
            var comment = await _unitOfWork.CommentRepo.GetByIdAsync(commentDTO.Id);
            _mapper.Map(commentDTO, comment);
            comment.UpdateDate = DateTime.Now;
            comment.Status = EntityStatus.Updated;
            return await _unitOfWork.CommentRepo.UpdateAsync(comment);
        }
    }
}
