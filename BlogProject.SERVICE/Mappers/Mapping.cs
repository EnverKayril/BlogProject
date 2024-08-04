using AutoMapper;
using BlogProject.CORE.CoreModels.Models;
using BlogProject.SERVICE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.Mappers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AppUser, AppUserDTO>()
                .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<ArticleDTO, Article>().ReverseMap();
            CreateMap<ArticleWithUserDTO, Article>();
        }
    }
}
