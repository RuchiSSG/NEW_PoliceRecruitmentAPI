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
    public class RFIDChestNoMappingService:IRFIDChestNoMappingService
    {
        RFIDChestNoMappingRepository _candidateRepository;
        public RFIDChestNoMappingService(RFIDChestNoMappingRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }
        public async Task<IActionResult> Get(RFIDChestNoMappingDto model)
        {
            return await _candidateRepository.Get(model);

        }
        public async Task<IActionResult> GetRFID(RFIDChestNoMappingDto model)
        {
            return await _candidateRepository.GetRFID(model);

        }
        public async Task<IActionResult> RFIDChestNoMapping(RFIDChestNoMappingDto model)
        {
            return await _candidateRepository.RFIDChestNoMapping(model);

        } 
        public async Task<IActionResult> RFIDRunningLog(RFIDChestNoMappingDto model)
        {
            return await _candidateRepository.RFIDRunningLog(model);

        }
    }
}
