using AutoMapper;
using BlogProject.CORE.CoreModels.Models;
using BlogProject.SERVICE.DTOs;
using BlogProject.SERVICE.Services.IServices;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser>userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<int> CountAsync()
        {
            if (_unitOfWork.ArticleRepo == null)
            {
                return 0;
            }
            return await _unitOfWork.AppUserRepo.CountAsync();
        }

        public int CreateAppUser(AppUserCreateDTO appUserCreateDTO)
        {
            var appUser = _mapper.Map<AppUser>(appUserCreateDTO);
            return _unitOfWork.AppUserRepo.Add(appUser);
        }

        public async Task<int> DeleteAppUser(string id)
        {
            var appUser = await _unitOfWork.AppUserRepo.GetByIdAsync(id);
            if (appUser is not null)
            {
                return _unitOfWork.AppUserRepo.Delete(appUser);
            }
            return 0;
        }

        public async Task<IEnumerable<AppUserDTO>> GetAllAppUserAsync()
        {
            var appUsers = await _unitOfWork.AppUserRepo.GetAllAsync();

            var appUserDTOs = new List<AppUserDTO>();

            foreach (var user in appUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userDto = _mapper.Map<AppUserDTO>(user);
                userDto.Roles = roles.ToList();

                appUserDTOs.Add(userDto);
            }

            return appUserDTOs;
        }

        public async Task<AppUserDTO> GetAppUserByIdAsync(string id)
        {
            var appUser = await _unitOfWork.AppUserRepo.GetByIdAsync(id);
            return _mapper.Map<AppUserDTO>(appUser);
        }

        public async Task<int> UpdateAppUserAsync(AppUserDTO appUserDTO)
        {
            var user = await _unitOfWork.AppUserRepo.GetByIdAsync(appUserDTO.Id);

            if (user != null)
            {
                user.UserName = appUserDTO.UserName;
                user.Email = appUserDTO.Email;
                user.PhoneNumber = appUserDTO.PhoneNumber;
                user.Photo = appUserDTO.Photo;

                return _unitOfWork.AppUserRepo.Update(user);
            }

            throw new InvalidOperationException("Kullanıcı bulunamadı");
        }
    }
}
