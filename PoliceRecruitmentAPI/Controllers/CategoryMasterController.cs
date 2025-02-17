using common;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.ApiServices;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Data;
using System.Globalization;

namespace PoliceRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExampleFilterAttribute]
    public class CategoryMasterController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ILogger<CategoryMasterController> _logger;
        public readonly ICategoryMasterService _categoryMasterService;
        public CategoryMasterController(ILogger<CategoryMasterController> logger, IConfiguration configuration, ICategoryMasterService categoryMasterService)
        {
            _logger = logger;
            _configuration = configuration;
            _categoryMasterService = categoryMasterService;
        }
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] CategoryMasterDto user)
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
                //else
                //{
                //    user.BaseModel.OperationType = "Update";
                //}
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("CategoryName", typeof(string));
                //dataTable.Columns.Add("maxValue", typeof(string));
                //dataTable.Columns.Add("score", typeof(decimal));
                //dataTable.Columns.Add("gender", typeof(string));
                //dataTable.Columns.Add("category", typeof(string));


                foreach (var categortdata in user.Categoryins)
                {
                    dataTable.Rows.Add(
                        categortdata.CategoryName
                         
                        
                    );
                }
                user.Categoryins = null;
                user.DataTable = dataTable;
                var result = await _categoryMasterService.Get(user);
                return result;
            }
            catch (Exception ex)
            {
                // Construct detailed error response
                var errorResponse = new LogErrorResponse
                {
                    ErrorId = Guid.NewGuid().ToString("N"),
                    Timestamp =DateTime.Now,
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

                // Return structured error response
                return new JsonResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] CategoryMasterDto model)
        {
            try
            {
                //ShotPutDto model = new ShotPutDto();
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "Get";

                dynamic userDetail = await _categoryMasterService.Get(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                // Construct detailed error response
                var errorResponse = new LogErrorResponse
                {
                    ErrorId = Guid.NewGuid().ToString("N"),
                    Timestamp =DateTime.Now,
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] string userid, [FromQuery] string recConfId)
        {

            CategoryMasterDto model = new CategoryMasterDto();

            model.recConfId=recConfId;
            model.UserId=userid;
            try
            {
                //ShotPutDto model = new ShotPutDto();
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAll";

                dynamic userDetail = await _categoryMasterService.Category(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                // Construct detailed error response
                var errorResponse = new LogErrorResponse
                {
                    ErrorId = Guid.NewGuid().ToString("N"),
                    Timestamp =DateTime.Now,
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

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] CategoryMasterDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "Delete";

                var result = await _categoryMasterService.Category(model);
                return result;
            }
            catch (Exception ex)
            {
                // Construct detailed error response
                var errorResponse = new LogErrorResponse
                {
                    ErrorId = Guid.NewGuid().ToString("N"),
                    Timestamp =DateTime.Now,
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
