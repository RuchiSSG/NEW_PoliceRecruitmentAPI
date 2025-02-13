using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Services.Interfaces
{
	public interface IRecruitmentConfig
	{
		public Task<IActionResult> RecruitConfig(RecruitmentConfigDto model);
		public Task<IActionResult> Get(RecruitmentConfigDto model);
	}
}
	