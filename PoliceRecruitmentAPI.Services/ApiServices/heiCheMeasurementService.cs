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
	public class heiCheMeasurementService: IheiCheMeasurement
	{

		heiCheMeasurementRepositry _heiCheMeasurementRepository;
		public heiCheMeasurementService(heiCheMeasurementRepositry heiCheMeasurementRepository)
		{
			_heiCheMeasurementRepository = heiCheMeasurementRepository;
		}
		public async Task<IActionResult> Measurement(heiCheMeasurement model)
		{
			return await _heiCheMeasurementRepository.Measurement(model);

		}
		public async Task<IActionResult> Get(heiCheMeasurement model)
		{
			return await _heiCheMeasurementRepository.Get(model);

		}

	}
}
 