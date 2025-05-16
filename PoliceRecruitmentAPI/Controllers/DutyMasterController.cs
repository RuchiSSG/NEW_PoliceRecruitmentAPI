using common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Data;
using System.Globalization;

namespace PoliceRecruitmentAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ExampleFilterAttribute]
	public class DutyMasterController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ILogger<DutyMasterController> _logger;
		public readonly IDutyMasterService _dutymaster;

		public DutyMasterController(ILogger<DutyMasterController> logger, IConfiguration configuration, IDutyMasterService dutymaster)
		{
			_logger = logger;
			_configuration = configuration;
			_dutymaster = dutymaster;
		}
		[HttpGet("GetAll")]
		public async Task<IActionResult> GetDuty([FromQuery]DutyMasterDto user)
		{
			try
			{
				
				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}
				user.BaseModel.OperationType = "GetAll";

				var createduser = await _dutymaster.Duty(user);
				return createduser;
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

        [HttpGet("GetDutyName")]
        public async Task<IActionResult> GetDutyName([FromQuery] DutyMasterDto user)
        {
            try
            {

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetDutyName";

                var createduser = await _dutymaster.Duty(user);
                return createduser;
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


        [HttpGet("GetDutyById")]
        public async Task<IActionResult> GetDutyById([FromQuery] DutyMasterDto user)
        {
            try
            {

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "Get";

                var createduser = await _dutymaster.Duty(user);
                return createduser;
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
		public async Task<IActionResult> Getmenu([FromQuery]DutyMasterDto user)
		{
			try
			{
				

				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}
				user.BaseModel.OperationType = "GetMenu";

				var createduser = await _dutymaster.Duty(user);
				return createduser;
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

		

		[HttpPost]
		public async Task<IActionResult> InsertDuty([FromBody] DutyMasterDto user)
		{
			try
			{
				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}

				if (user.d_id == null)
				{
					user.BaseModel.OperationType = "Insert";
				}
				else
				{
					user.BaseModel.OperationType = "Update";
				}
				user.d_createddate = DateTime.Now;
				user.d_updateddate = DateTime.Now;
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("a_menuid", typeof(string));
				dataTable.Columns.Add("a_addaccess", typeof(string));
				dataTable.Columns.Add("a_editaccess", typeof(string));
				dataTable.Columns.Add("a_deleteaccess", typeof(string));
				dataTable.Columns.Add("a_viewaccess", typeof(string));
				dataTable.Columns.Add("a_workflow", typeof(string));
				if (user.d_id != null && user.Privilage == null)
				{
					user.BaseModel.OperationType = "UpdateStatus";
				}
				else
				{
					foreach (var privilage in user.Privilage)
					{
						dataTable.Rows.Add(
							privilage.a_menuid,
							privilage.a_addaccess,
							privilage.a_editaccess,
							privilage.a_deleteaccess,
							privilage.a_viewaccess,
							privilage.a_workflow
						);
					}
					// user.Privilage = null;
					user.DataTable = dataTable;
				}
				var createduser = await _dutymaster.Get(user);
				return createduser;
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

		[HttpPost("DeleteDuty")]
		public async Task<IActionResult> DeleteDuty([FromBody] DutyMasterDto user)
		{
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }

                user.BaseModel.OperationType = "Delete";
                var productDetails = await _dutymaster.Get(user);
                return productDetails;
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
