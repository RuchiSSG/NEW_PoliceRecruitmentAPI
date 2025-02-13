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
	public class ParameterValueMasterService : IParameterValueMasterService
	{
		ParameterValueMasterRepository _parameterMastervValueRepository;
		public ParameterValueMasterService(ParameterValueMasterRepository parameterMasterValueRepository)
		{
			_parameterMastervValueRepository = parameterMasterValueRepository;
		}
		public async Task<IActionResult> ParameterValue(ParameterValueMasterDto model)
		{
			return await _parameterMastervValueRepository.ParameterValue(model);

		}

		public async Task<IActionResult> Get(ParameterValueMasterDto model)
		{
			return await _parameterMastervValueRepository.Get(model);

		}
	}
}
