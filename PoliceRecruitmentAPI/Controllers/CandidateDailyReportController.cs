using common;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.ApiServices;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Data;
using System.Text;

 
    namespace PoliceRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExampleFilterAttribute]
    public class CandidateDailyReportController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ILogger<CandidateDailyReportController> _logger;
        public readonly ICandidateDailyReportService _candidateService;
        public CandidateDailyReportController(ILogger<CandidateDailyReportController> logger, IConfiguration configuration, ICandidateDailyReportService candidateService)
        {
            _logger = logger;
            _configuration = configuration;
            _candidateService = candidateService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] CandidateDailyReportDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAll";

                dynamic userDetail = await _candidateService.Candidate(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

    }
}
