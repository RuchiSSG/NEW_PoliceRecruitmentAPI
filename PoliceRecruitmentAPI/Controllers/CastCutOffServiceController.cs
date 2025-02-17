using common;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.ApiServices;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Data;
using System.Globalization;
using System.Text;

namespace PoliceRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExampleFilterAttribute]
    public class CastCutOffServiceController : Controller
    {
        public IConfiguration _configuration;
        private readonly ILogger<CastCutOffServiceController> _logger;
        public readonly ICastCutOffService _CastCutOffService;
        public readonly ICategoryMasterService _categoryMasterService;
        public readonly IParameterValueMasterService _parameterValueMaster;
        public CastCutOffServiceController(ILogger<CastCutOffServiceController> logger, IConfiguration configuration, ICastCutOffService castCutOffService, ICategoryMasterService categoryMasterService, IParameterValueMasterService parameterValueMaster)
        {
            _logger = logger;
            _configuration = configuration;
            _CastCutOffService = castCutOffService;
            _categoryMasterService = categoryMasterService;
            _parameterValueMaster = parameterValueMaster;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] CastCutOffServiceDto user)
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
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("SubCategoryName", typeof(string));
                dataTable.Columns.Add("ParentCastId", typeof(string));
                dataTable.Columns.Add("CutOffPosition", typeof(string));
                dataTable.Columns.Add("Total", typeof(string));
                dataTable.Columns.Add("CastTotal", typeof(string));
                //dataTable.Columns.Add("NTC", typeof(string));
                //dataTable.Columns.Add("SBC", typeof(string));
                //dataTable.Columns.Add("Open", typeof(string));
                //dataTable.Columns.Add("NTB", typeof(decimal));
                //dataTable.Columns.Add("NTD", typeof(string));
                //dataTable.Columns.Add("VJ", typeof(string));



                foreach (var cutofftdata in user.Castsdata)
                {
                    dataTable.Rows.Add(
                        cutofftdata.SubCategoryName,
                        cutofftdata.ParentCastId,
                        cutofftdata.CutOffPosition,
                        cutofftdata.Total,
                         cutofftdata.CastTotal
                    //cutofftdata.NTC,
                    //cutofftdata.SBC,
                    //cutofftdata.Open,
                    //cutofftdata.NTB,
                    //cutofftdata.NTD,
                    //cutofftdata.VJ

                    );
                }
                user.Castsdata = null;
                user.DataTable = dataTable;
                var result = await _CastCutOffService.Get(user);
                return result;
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

        [HttpPost("InsertTestCutoff")]
        public async Task<IActionResult> InsertTestCutoff([FromBody] CastCutOffServiceDto user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                if (user.Id == null)
                {
                    user.BaseModel.OperationType = "InsertTestCutoff";
                }
                //else
                //{
                //    user.BaseModel.OperationType = "Update";
                //}
                DataTable dataTable = new DataTable();
                user.Castsdata = null;
                user.DataTable = dataTable;
                var result = await _CastCutOffService.Get(user);
                return result;
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
        //[HttpGet("Get")]
        //public async Task<IActionResult> Get([FromQuery] CastCutOffServiceDto model)
        //{
        //    try
        //    {
        //        //ShotPutDto model = new ShotPutDto();
        //        if (model.BaseModel == null)
        //        {
        //            model.BaseModel = new BaseModel();
        //        }
        //        model.BaseModel.OperationType = "Get";

        //        dynamic userDetail = await _CastCutOffService.Get(model);
        //        return userDetail;

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        //    }
        //}
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] string userid, [FromQuery] string recConfId,string CategoryId)
        {

            CastCutOffServiceDto model = new CastCutOffServiceDto();

            model.RecruitId = recConfId;
            model.UserId = userid;
            model.CategoryId = CategoryId;
            try
            {
                //ShotPutDto model = new ShotPutDto();
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAll";

                dynamic userDetail = await _CastCutOffService.Cast(model);

                model.BaseModel.OperationType = "GetSubCategory";

                dynamic categorydetail = await _CastCutOffService.Cast(model);
  
                var parameterValueMasterDto = new ParameterValueMasterDto
                {
                    pv_isactive="1",
                   pv_parameterid= "562f4f41-1127-4510-968f-08365867759f",
                    UserId = userid,
                    BaseModel = new BaseModel { OperationType = "GetAll" }
                };
                dynamic CastDetail = await _parameterValueMaster.ParameterValue(parameterValueMasterDto);
                dynamic tempoutdetail = userDetail.Value.Outcome;
                //dynamic Outcome = userDetail.Outcome;

                dynamic finaldata = new { Data = userDetail, categoryList = categorydetail ,Castdata= CastDetail };
                dynamic Result = new { Data = finaldata, UserId = userid, Outcome = tempoutdetail };
                return Ok(Result);

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

        [HttpGet("GetSubCategory")]
        public async Task<IActionResult> GetSubCategory([FromQuery] string userid, [FromQuery] string recConfId)
        {

            CastCutOffServiceDto model = new CastCutOffServiceDto();

            model.RecruitId = recConfId;
            model.UserId = userid;
            try
            {
                //ShotPutDto model = new ShotPutDto();
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetSubCategory";

                dynamic userDetail = await _CastCutOffService.Cast(model);
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

        [HttpGet("GetAllTests")]
        public async Task<IActionResult> GetAllTests([FromQuery] string userid, [FromQuery] string recConfId)
        {

            CastCutOffServiceDto model = new CastCutOffServiceDto();

            model.RecruitId = recConfId;
            model.UserId = userid;
            try
            {
                //ShotPutDto model = new ShotPutDto();
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAllTests";

                dynamic userDetail = await _CastCutOffService.Cast1(model);
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

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] CastCutOffServiceDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "Delete";

                var result = await _CastCutOffService.Cast(model);
                return result;
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
