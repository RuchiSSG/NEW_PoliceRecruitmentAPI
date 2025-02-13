using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Services.Interfaces
{
    public interface IRFIDChestNoMappingService
    {
        public Task<IActionResult> Get(RFIDChestNoMappingDto model);
        public Task<IActionResult> GetRFID(RFIDChestNoMappingDto model);
        public Task<IActionResult> RFIDChestNoMapping(RFIDChestNoMappingDto model);
		public Task<IActionResult> RFIDRunningLog(RFIDChestNoMappingDto model);
	}
}
