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
    public class OmrMasterService : IOmrMasterService
    {
        OmrMasterRepository _OmrMasterRepository;
        public OmrMasterService(OmrMasterRepository OmrMasterRepository)
        {
            _OmrMasterRepository = OmrMasterRepository;
        }
        public async Task<IActionResult> Get(OmrMasterDto model)
        {
            return await _OmrMasterRepository.Get(model);

        }

        public async Task<IActionResult> OMR(OmrMasterDto model)
        {

            return await _OmrMasterRepository.OMR(model);
        }
    }
}
