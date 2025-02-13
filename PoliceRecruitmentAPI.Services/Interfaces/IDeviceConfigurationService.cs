using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Services.Interfaces
{
    public interface IDeviceConfigurationService
    {
        public Task<IActionResult> Get(DeviceConfigurationDto model);
        public Task<IActionResult> DeviceConf(DeviceConfigurationDto model);
    }
}
