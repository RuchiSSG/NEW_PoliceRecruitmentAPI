using common;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Globalization;

namespace PoliceRecruitmentAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [ExampleFilterAttribute]
    public class AdmissionCardController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ILogger<AdmissionCardController> _logger;
		public readonly IAdmissionCardService _candidateService;

       
        public AdmissionCardController(ILogger<AdmissionCardController> logger, IConfiguration configuration, IAdmissionCardService candidateService)
		{
			_logger = logger;
			_configuration = configuration;
			_candidateService = candidateService;
		}

		[HttpGet("GetCard")]
		public async Task<IActionResult> GetCard([FromQuery]CandidateDto model)
        {
			try
			{

                if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "GetAdmissionCard";

				dynamic userDetail = await _candidateService.Get(model);

				return userDetail;

			}
            catch (Exception ex)
            {
                // Using LogErrorResponse model for cleaner code
                var errorResponse = new LogErrorResponse
                {
                    ErrorId = Guid.NewGuid().ToString("N"),
                    Timestamp =  DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    OperationType = model.BaseModel?.OperationType ?? "Unknown"
                };
                _logger.LogError(ex, "{SeparatorLine}\n"+"Error ID: {ErrorId}\t" +"DateTime: {FormattedTimestamp}\n" +"Error Message: {Message}\n" +"Stack Trace: {StackTrace}\n"+"{SeparatorLine}",
                                   LogErrorResponse.SEPARATOR_LINE,
                                   errorResponse.ErrorId,
                                    errorResponse.FormattedTimestamp,
                                   errorResponse.Message,
                                   errorResponse.StackTrace,
                                   LogErrorResponse.SEPARATOR_LINE
                               );


                return new JsonResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
	}
}
