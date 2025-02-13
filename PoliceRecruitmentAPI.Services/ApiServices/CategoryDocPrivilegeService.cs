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
    public class CategoryDocPrivilegeService: ICategoryDocPrivilegeService
    {
        CategoryDocPrivilegeRepository _CategoryDocPrivilegeRepository;
        public CategoryDocPrivilegeService(CategoryDocPrivilegeRepository categoryDocPrivilegeRepository)
        {
            _CategoryDocPrivilegeRepository =categoryDocPrivilegeRepository;
        }
        public async Task<IActionResult> Category(CategoryPrirvilegeDto model)
        {
            return await _CategoryDocPrivilegeRepository.Category(model);

        }

        public async Task<IActionResult> Get(CategoryPrirvilegeDto model)
        {
            return await _CategoryDocPrivilegeRepository.Get(model);

        }
    }
}
