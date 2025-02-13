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
	public class DutyMasterService:IDutyMasterService
	{
		DutyMasterRepository _dutyMasterRepository;
		public DutyMasterService(DutyMasterRepository dutyMasterRepository)
		{
			_dutyMasterRepository = dutyMasterRepository;
		}
		public async Task<IActionResult> Duty(DutyMasterDto model)
		{
			return await _dutyMasterRepository.Duty(model);

		}

		public async Task<IActionResult> Get(DutyMasterDto model)
		{
			return await _dutyMasterRepository.Get(model);

		}

	}
}
