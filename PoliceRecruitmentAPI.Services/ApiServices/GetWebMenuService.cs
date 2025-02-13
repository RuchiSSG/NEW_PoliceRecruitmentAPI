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
	public class GetWebMenuService: IGetWebMenuService
	{
		GetWebMenuRepository _getWebMenuRepository;
		public GetWebMenuService(GetWebMenuRepository getWebMenuRepository)
		{
			_getWebMenuRepository = getWebMenuRepository;
		}
		public async Task<IActionResult> GetWebMenu(GetWebMenuDto model)
		{
			return await _getWebMenuRepository.GetWebMenu(model);

		}

	}
}
