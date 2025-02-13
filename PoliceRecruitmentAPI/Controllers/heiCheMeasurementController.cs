using common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Core.Repository;
using PoliceRecruitmentAPI.Services.ApiServices;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Data;

namespace PoliceRecruitmentAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ExampleFilterAttribute]
	public class heiCheMeasurementController : ControllerBase
	{

		public IConfiguration _configuration;
		private readonly ILogger<heiCheMeasurementController> _logger;
		public readonly IheiCheMeasurement _heiCheMeasurement;

		public heiCheMeasurementController(ILogger<heiCheMeasurementController> logger, IConfiguration configuration, IheiCheMeasurement heightmeasurment)
		{
			_logger = logger;
			_configuration = configuration;
			_heiCheMeasurement = heightmeasurment;
		}
		[HttpPost("Insert")]
		public async Task<IActionResult> Insert([FromBody] heiCheMeasurement user)
		{
			try
			{
				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}
				user.BaseModel.OperationType = "Insert";
				var result = await _heiCheMeasurement.Measurement(user);
				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}
		[HttpGet("Get")]
		public async Task<IActionResult> Get([FromQuery] heiCheMeasurement model)
		{
			try
			{
				//heiCheMeasurement model = new heiCheMeasurement();
				//model.UserId = UserId;
				//model.hid = id;
				//model.candidate_id = CandidateId;
				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "Get";

				dynamic userDetail = await _heiCheMeasurement.Get(model);

				return userDetail;

			}
			catch (Exception ex)
			{
				return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
			}
		}
        [HttpGet("GetHistory")]
        public async Task<IActionResult> GetHistory([FromQuery] heiCheMeasurement model)
        {
            try
            {
                
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetHistory";

                dynamic userDetail = await _heiCheMeasurement.Measurement(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
        [HttpGet("GetAllConfig")]
        public async Task<IActionResult> GetAllConfig([FromQuery] heiCheMeasurement model)
        {
            try
            {
               
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAllConfig";

                dynamic userDetail = await _heiCheMeasurement.Measurement(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }


        [HttpPost("InsertConfig")]
        public async Task<IActionResult> InsertConfig([FromBody] heiCheMeasurement user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "UpdateConfig";
                var result = await _heiCheMeasurement.Measurement(user);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
