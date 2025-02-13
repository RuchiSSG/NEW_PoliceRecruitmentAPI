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
	public class RecruitmentEventService: IRecruitmentEventService
	{
		RecruitmentEventRepository _recruitmentEventRepository;
		public RecruitmentEventService(RecruitmentEventRepository recruitmentEventRepository)
		{
			_recruitmentEventRepository = recruitmentEventRepository;
		}
		public async Task<IActionResult> RecruitEvent(RecruitmentEventDto model)
		{
			return await _recruitmentEventRepository.RecruitEvent(model);

		}

		public async Task<IActionResult> Get(RecruitmentEventDto model)
		{
			return await _recruitmentEventRepository.Get(model);

		}
        public async Task<IActionResult> GetSign(AllEventSignDto model)
        {
            return await _recruitmentEventRepository.GetSign(model);

        }

    }
}
