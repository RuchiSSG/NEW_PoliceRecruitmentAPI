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
    public class CastCutOffService : ICastCutOffService
    {
        CastCutOffServiceRepository _castCutOffServiceRepository;
        public CastCutOffService(CastCutOffServiceRepository castCutOffServiceRepository)
        {
            _castCutOffServiceRepository = castCutOffServiceRepository;
        }
        public async Task<IActionResult> Get(CastCutOffServiceDto model)
        {
            return await _castCutOffServiceRepository.Get(model);

        }

        public async Task<IActionResult> Cast(CastCutOffServiceDto model)
        {

            return await _castCutOffServiceRepository.Cast(model);
        }

        public async Task<IActionResult> Cast1(CastCutOffServiceDto model)
        {

            return await _castCutOffServiceRepository.Cast1(model);
        }
    }
}
