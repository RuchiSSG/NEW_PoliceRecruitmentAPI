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
    public class DeviceConfigurationService:IDeviceConfigurationService
    {
        DeviceConfigurationRepository _deviceConfigurationRepository;
        public DeviceConfigurationService(DeviceConfigurationRepository deviceConfigurationRepository)
        {
            _deviceConfigurationRepository = deviceConfigurationRepository;
        }
        public async Task<IActionResult> DeviceConf(DeviceConfigurationDto model)
        {
            return await _deviceConfigurationRepository.DeviceConf(model);

        }

        public async Task<IActionResult> Get(DeviceConfigurationDto model)
        {
            return await _deviceConfigurationRepository.Get(model);

        }
    }
}
