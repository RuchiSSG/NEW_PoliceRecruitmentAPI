using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Services.Interfaces
{ 
    public interface IParameterValueMasterService
    {
        public Task<IActionResult> ParameterValue(ParameterValueMasterDto model);
        public Task<IActionResult> Get(ParameterValueMasterDto model);
    }
}
