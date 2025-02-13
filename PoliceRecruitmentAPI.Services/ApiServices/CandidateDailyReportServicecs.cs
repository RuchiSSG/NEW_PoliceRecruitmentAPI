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
    public class CandidateDailyReportServicecs: ICandidateDailyReportService
    {
        CandidateDailyReportRepository _candidateDailyReportRepository;
        public CandidateDailyReportServicecs(CandidateDailyReportRepository candidateDailyReportRepository)
        {
            _candidateDailyReportRepository = candidateDailyReportRepository;
        }
        public async Task<IActionResult> Get(CandidateDailyReportDto model)
        {
            return await _candidateDailyReportRepository.Get(model);

        }

        public async Task<IActionResult> Candidate(CandidateDailyReportDto model)
        {

            return await _candidateDailyReportRepository.Candidate(model);
        }
    }
}
