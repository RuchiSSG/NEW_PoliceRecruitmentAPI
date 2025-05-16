using common;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Globalization;

namespace PoliceRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[ExampleFilterAttribute]
	public class DashboardController : ControllerBase
    {
		public IConfiguration _configuration;
		private readonly ILogger<DashboardController> _logger;
		public readonly IDashboardService _candidateService;

		public DashboardController(ILogger<DashboardController> logger, IConfiguration configuration, IDashboardService candidateService)
		{
			_logger = logger;
			_configuration = configuration;
			_candidateService = candidateService;
		}

		[HttpGet("Get")]
		public async Task<IActionResult> GetCard([FromQuery] DashboardDto model)
		{
			try
			{

				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "Get";

				dynamic userDetail = await _candidateService.Get(model);

				return userDetail;

			}
            catch (Exception ex)
            {
                // Construct detailed error response
                var errorResponse = new LogErrorResponse
                {
                    ErrorId = Guid.NewGuid().ToString("N"),
                    Timestamp = DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    OperationType = model?.BaseModel?.OperationType ?? "Unknown"
                };

                // Log error details
                _logger.LogError(ex, "{SeparatorLine}\n"+"Error ID: {ErrorId}\t" +"DateTime: {FormattedTimestamp}\n" +"Error Message: {Message}\n" +"Stack Trace: {StackTrace}\n"+"{SeparatorLine}",
                     LogErrorResponse.SEPARATOR_LINE,
                     errorResponse.ErrorId,
                     errorResponse.FormattedTimestamp,
                     errorResponse.Message,
                     errorResponse.StackTrace,
                     LogErrorResponse.SEPARATOR_LINE
                 );
                // Return structured error response
                return new JsonResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
	}
}
