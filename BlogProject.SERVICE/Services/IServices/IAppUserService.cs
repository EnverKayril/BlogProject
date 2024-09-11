using BlogProject.SERVICE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Services.IServices
{
    public interface IAppUserService
    {
        Task<AppUserDTO> GetAppUserByIdAsync(string id);
        Task<IEnumerable<AppUserDTO>> GetAllAppUserAsync();
        int CreateAppUser(AppUserCreateDTO appUserCreateDTO);
        Task<int> UpdateAppUserAsync(AppUserDTO appUserDTO);
        Task<int> DeleteAppUser(string id);
        Task<int> CountAsync();
    }
}
