using AutoMapper;
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
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public int CreateCategoryAsync(CategoryCreateDTO categoryDTO)
        {

            //var category = new Category { Name = categoryDTO.Name };
            var category = mapper.Map<Category>(categoryDTO);
            return _unitOfWork.CategoryRepo.Add(category);
        }

        public async Task<int> DeleteCategoryAsync(string id)
        {
            var category = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
            category.DeleteDate = DateTime.Now;
            category.Status = CORE.CoreModels.Enums.EntityStatus.Deleted;
            return _unitOfWork.CategoryRepo.Delete(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepo.GetAllAsync();
            return mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(string id)
        {
            var category = await _unitOfWork.CategoryRepo.GetByIdAsync(id);
            return mapper.Map<CategoryDTO>(category);
        }

        public int UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);
            category.UpdateDate = DateTime.Now;
            category.Status = CORE.CoreModels.Enums.EntityStatus.Updated;
            return _unitOfWork.CategoryRepo.Update(category);
        }
    }
}
