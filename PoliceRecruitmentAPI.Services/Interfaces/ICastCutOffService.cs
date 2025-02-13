using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Services.Interfaces
{
    public interface ICastCutOffService
    {
        public Task<IActionResult> Cast(CastCutOffServiceDto model);
        public Task<IActionResult> Cast1(CastCutOffServiceDto model);
        public Task<IActionResult> Get(CastCutOffServiceDto model);
    }
}
