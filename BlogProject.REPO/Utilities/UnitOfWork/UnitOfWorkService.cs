using AutoMapper;
using BlogProject.SERVICE.Services;
using BlogProject.SERVICE.Services.IServices;
using BlogProject.SERVICE.Utilities.IUnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Utilities.UnitOfWork
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork _unitOfWorkService;
        private readonly IMapper _mapper;

        public UnitOfWorkService(IUnitOfWork unitOfWorkService, IMapper mapper)
        {
            _unitOfWorkService = unitOfWorkService;
            _mapper = mapper;
            CategoryService = new CategoryService(_unitOfWorkService, _mapper);
        }

        public ICategoryService CategoryService { get; }
    }
}
