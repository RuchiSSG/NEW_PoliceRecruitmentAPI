using common;
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
	public class GetWebMenuController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ILogger<GetWebMenuController> _logger;
		public readonly IGetWebMenuService _getWebMenuService;

		public GetWebMenuController(ILogger<GetWebMenuController> logger, IConfiguration configuration,IGetWebMenuService getWebMenuService)
		{
			_logger = logger;
			_configuration = configuration;
			_getWebMenuService = getWebMenuService;
		}



		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll([FromQuery]GetWebMenuDto user)
		{
			try
			{
				

				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}

				user.BaseModel.OperationType = "GetWebMenu";
				
				var createduser = await _getWebMenuService.GetWebMenu(user);
				var data = ((Microsoft.AspNetCore.Mvc.ObjectResult)createduser).Value;
				return Ok(data);
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
		[HttpGet("GetMenu")]
		public async Task<IActionResult> GetMenu([FromQuery] GetWebMenuDto user)
		{
			try
			{


				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}

				user.BaseModel.OperationType = "GetMenu";

				var createduser = await _getWebMenuService.GetWebMenu(user);
				var data = ((Microsoft.AspNetCore.Mvc.ObjectResult)createduser).Value;
				return Ok(data);
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
	}
}
