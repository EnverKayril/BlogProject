using BlogProject.SERVICE.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Services.IServices
{
    public interface ICategoryService
    {
        Task<CategoryDTO> GetCategoryByIdAsync(string id);
        Task<IEnumerable<CategoryDTO>> GetAllActiveCategoriesAsync();
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        int CreateCategoryAsync(CategoryCreateDTO categoryDTO);
        int UpdateCategoryAsync(CategoryDTO categoryDTO);
        Task<int> DeleteCategoryAsync(string id);
        Task<int> CountAsync();
    }
}
