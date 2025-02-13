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
	public class RecruitmentConfigService:IRecruitmentConfig
	{
		RecruitmentConfigRepository _recruitmentConfigRepository;

		public RecruitmentConfigService(RecruitmentConfigRepository recruitmentConfigRepository)
		{
			_recruitmentConfigRepository = recruitmentConfigRepository;
		}

		public async Task<IActionResult> RecruitConfig(RecruitmentConfigDto model)
		{
			return await _recruitmentConfigRepository.RecruitConfig(model);

		}

		public async Task<IActionResult> Get(RecruitmentConfigDto model)
		{
			return await _recruitmentConfigRepository.Get(model);

		}

	}
}
