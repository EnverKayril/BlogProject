﻿using AutoMapper;
using BlogProject.CORE.CoreModels.Models;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Services.IServices;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CountAsync()
        {
            if (_unitOfWork.CategoryRepo == null)
            {
                return 0;
            }
            return await _unitOfWork.CategoryRepo.CountAsync();
        }

        public int CreateCategoryAsync(CategoryCreateDTO categoryDTO)
        {
            //var category = new Category { Name = categoryDTO.Name };
            var category = _mapper.Map<Category>(categoryDTO);
            return _unitOfWork.CategoryRepo.Add(category);
        }

        public async Task<int> DeleteCategoryAsync(string id)
        {
            var category = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
            category.DeleteDate = DateTime.Now;
            category.Status = CORE.CoreModels.Enums.EntityStatus.Deleted;
            return _unitOfWork.CategoryRepo.Delete(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllActiveCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepo.GetAllAsync(c => c.Status != CORE.CoreModels.Enums.EntityStatus.Deleted);
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(string id)
        {
            var category = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public int UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            category.UpdateDate = DateTime.Now;
            category.Status = CORE.CoreModels.Enums.EntityStatus.Updated;
            return _unitOfWork.CategoryRepo.Update(category);
        }
    }
}
