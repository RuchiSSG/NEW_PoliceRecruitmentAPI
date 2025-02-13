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
	public class ParameterMasterService:IParameterMasterService
	{
		ParameterMasterRepository _parameterMasterRepository;
		public ParameterMasterService(ParameterMasterRepository parameterMasterRepository)
		{
			_parameterMasterRepository = parameterMasterRepository;
		}
		public async Task<IActionResult> Parameter(ParameterMasterDto model)
		{
			return await _parameterMasterRepository.Parameter(model);

		}

		public async Task<IActionResult> Get(ParameterMasterDto model)
		{
			return await _parameterMasterRepository.Get(model);

		}
	}
}
