﻿using AutoMapper;
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
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
            CreateMap<AppUserCreateDTO, AppUser>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<CategoryCreateDTO, Category>();

            CreateMap<ArticleDTO, Article>().ReverseMap();
            CreateMap<ArticleCreateDTO, Article>();
            CreateMap<ArticleWithUserDTO, Article>();
            CreateMap<Article, ArticleDetailDTO>().ReverseMap();

            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<RoleCreateDTO, Role>();

            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<CommentCreateDTO, Comment>();
            CreateMap<CommentWithUserDTO, Comment>();
        }
    }
}
