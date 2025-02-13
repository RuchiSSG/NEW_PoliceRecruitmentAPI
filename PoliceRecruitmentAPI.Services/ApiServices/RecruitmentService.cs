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
	public class RecruitmentService:IRecruitmentService
	{
		RecruitmentRepository _recruitmentRepository;

		public RecruitmentService(RecruitmentRepository recruitmentRepository)
		{
			_recruitmentRepository = recruitmentRepository;
		}

		public async Task<IActionResult> Recruit(RecruitmentDto model)
		{
			return await _recruitmentRepository.Recruit(model);

		}

		public async Task<IActionResult> Get(RecruitmentDto model)
		{
			return await _recruitmentRepository.Get(model);

		}

	}
}
