using common;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.ApiServices;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Globalization;

namespace PoliceRecruitmentAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ExampleFilterAttribute]
	public class ShotPutController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ILogger<ShotPutController> _logger;
		public readonly IShotPutService _shotput;

		public ShotPutController(ILogger<ShotPutController> logger, IConfiguration configuration, IShotPutService shotput)
		{
			_logger = logger;
			_configuration = configuration;
			_shotput = shotput;
		}




		[HttpPost("Insert")]
		public async Task<IActionResult> Insert([FromBody] ShotPutDto user)
		{
			try
			{
				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}
				if (user.Id == null)
				{
					user.BaseModel.OperationType = "Insert";
				}
				else
				{
					user.BaseModel.OperationType = "Update";
				}
				var result = await _shotput.ShotPut(user);
				return result;
			}
            catch (Exception ex)
            {
                // Using LogErrorResponse model for cleaner code
                var errorResponse = new LogErrorResponse
                {
                    ErrorId = Guid.NewGuid().ToString("N"),
                    Timestamp = DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    OperationType = user?.BaseModel?.OperationType ?? "Unknown"
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

                return new JsonResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

        }

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll([FromQuery] ShotPutDto model)
		{
			try
			{
				//ShotPutDto model = new ShotPutDto();
				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "GetAll";

				dynamic userDetail = await _shotput.ShotPut(model);
				return userDetail;

			}
            catch (Exception ex)
            {
                // Using LogErrorResponse model for cleaner code
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
                return new JsonResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


		[HttpGet("Get")]
		public async Task<IActionResult> Get([FromQuery] ShotPutDto model)
		{
			try
			{
				//ShotPutDto model = new ShotPutDto();
				//model.Id = Id;
				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "Get";

				dynamic userDetail = await _shotput.ShotPut(model);

				return userDetail;

			}
            catch (Exception ex)
            {
                // Using LogErrorResponse model for cleaner code
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

                return new JsonResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
	}
}
