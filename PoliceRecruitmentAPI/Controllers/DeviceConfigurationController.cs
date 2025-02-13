using common;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.Interfaces;

namespace PoliceRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExampleFilterAttribute]
    public class DeviceConfigurationController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ILogger<DeviceConfigurationController> _logger;
        public readonly IDeviceConfigurationService _deviceConfigurationService;

        public DeviceConfigurationController(ILogger<DeviceConfigurationController> logger, IConfiguration configuration, IDeviceConfigurationService deviceConfigurationService)
        {
            _logger = logger;
            _configuration = configuration;
            _deviceConfigurationService = deviceConfigurationService;
        }


        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] DeviceConfigurationDto user, [FromQuery] string sessionid, [FromQuery] string ipaddress)
        {
            try
            {

                user.sessionid=sessionid;
                user.ipaddress = ipaddress;
                     
                
                if (user.BaseModel == null)
                {

                    user.BaseModel = new BaseModel();
                }

                user.BaseModel.OperationType = user.Id == null ? "Insert" : "Update";

                var result = await _deviceConfigurationService.DeviceConf(user);

                // Check if result is a List<object> and contains any items
                if (result is ObjectResult objectResult && objectResult.Value is List<object> resultList && resultList.Any())
                {
                    var firstItem = resultList[0];
                    // Use reflection to safely get the UserId property if it exists
                    var userIdProperty = firstItem.GetType().GetProperty("UserId");
                    if (userIdProperty != null)
                    {
                        var userId = userIdProperty.GetValue(firstItem)?.ToString();
                        if (!string.IsNullOrEmpty(userId))
                        {
                            // Use the UserId value as needed
                            // For example, you could add it to the response
                            return Ok(new { UserId = userId, Message = "Operation successful" });
                        }
                    }
                }

                // If we couldn't extract a UserId, just return the original result
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in Insert method");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }


        //[HttpPost("Insert")]
        //public async Task<IActionResult> Insert([FromBody] DeviceConfigurationDto user)
        //{
        //    try
        //    {
        //        if (user.BaseModel == null)
        //        {
        //            user.BaseModel = new BaseModel();
        //        }
        //        if (user.Id == null)
        //        {
        //            user.BaseModel.OperationType = "Insert";
        //        }
        //        else
        //        {
        //            user.BaseModel.OperationType = "Update";
        //        }
        //        var result = await _deviceConfigurationService.DeviceConf(user);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}



        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] string userid, [FromQuery] string deviceid, [FromQuery] string sessionid, [FromQuery] string ipaddress)
        {
            try
            {
                //ShotPutDto model = new ShotPutDto();
                DeviceConfigurationDto model = new DeviceConfigurationDto
                {
                    UserId = userid,
                    DeviceId=deviceid,
                    sessionid=sessionid,
                    ipaddress = ipaddress,
                    BaseModel = new BaseModel
                    {
                        OperationType = "Get"
                    }
                };


                dynamic userDetail = await _deviceConfigurationService.Get(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] string userid, [FromQuery] string deviceid, [FromQuery] string sessionid, [FromQuery] string ipaddress)
        {
            
            try
            {
                DeviceConfigurationDto model = new DeviceConfigurationDto
                {
                    UserId = userid,
                    DeviceId=deviceid,
                    sessionid=sessionid,
                    ipaddress = ipaddress,
                    BaseModel = new BaseModel
                    {
                        OperationType = "GetAll"
                    }
                };
                 
                dynamic userDetail = await _deviceConfigurationService.Get(model);
                return userDetail;
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}
