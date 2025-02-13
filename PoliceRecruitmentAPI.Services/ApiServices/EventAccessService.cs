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
    public class EventAccessService : IEventAccessService
    {
        EventAccessRepository _EventAccessRepository;
        public EventAccessService(EventAccessRepository EventAccessRepository)
        {
            _EventAccessRepository = EventAccessRepository;
        }
        public async Task<IActionResult> Event(EventAccessMasterDto model)
        {
            return await _EventAccessRepository.Event(model);

        }

        public async Task<IActionResult> Get(EventAccessMasterDto model)
        {
            return await _EventAccessRepository.Get(model);

        }
    }
}
