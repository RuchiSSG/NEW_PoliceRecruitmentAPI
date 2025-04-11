using PoliceRecruitmentAPI.Services.ApiServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoliceRecruitmentAPI.Core;
using PoliceRecruitmentAPI.Core.ModelDtos;
using Microsoft.AspNetCore.Mvc;

namespace PoliceRecruitmentAPI.Services.Interfaces
{
    public interface IRfidRepository
    {
        Task StartListenerAsync();
        Task StopListener();
        Task<IActionResult> GetLatestTagAsync(RfidOutcome rfidTagDto);
        void SetParameters(string accessToken, string refreshToken, string userid, string recruitid);
    }
}
