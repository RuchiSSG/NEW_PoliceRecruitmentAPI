using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Core.Repository;
using PoliceRecruitmentAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Services.ApiServices
{
    public class CategoryMasterService : ICategoryMasterService
    {
        CategoryMasterRepository _CategoryMasterRepository;
        public CategoryMasterService(CategoryMasterRepository CategoryMasterRepository)
        {
            _CategoryMasterRepository = CategoryMasterRepository;
        }
        public async Task<IActionResult> Category(CategoryMasterDto model)
        {
            return await _CategoryMasterRepository.Category(model);

        }

        public async Task<IActionResult> Get(CategoryMasterDto model)
        {
            return await _CategoryMasterRepository.Get(model);

        }
    }
    
}
