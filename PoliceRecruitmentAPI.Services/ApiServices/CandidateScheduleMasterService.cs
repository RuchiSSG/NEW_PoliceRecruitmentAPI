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
    public class CandidateScheduleMasterService : ICandidateScheduleMasterService
    {

        CandidateScheduleMasterRepository _candidatescheduleRepository;
        public CandidateScheduleMasterService(CandidateScheduleMasterRepository candidateRepository)
        {
            _candidatescheduleRepository = candidateRepository;
        }
        public async Task<IActionResult> Get(CandidateScheduleMasterDto model)
        {
            return await _candidatescheduleRepository.Get(model);

        }
        public async Task<IActionResult> CandidateSchedule(CandidateScheduleMasterDto model)
        {
            return await _candidatescheduleRepository.CandidateSchedule(model);

        }
    }
}
