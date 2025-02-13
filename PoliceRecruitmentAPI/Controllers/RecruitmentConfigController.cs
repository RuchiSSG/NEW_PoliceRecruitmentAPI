using common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.Interfaces;

namespace PoliceRecruitmentAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ExampleFilterAttribute]
	public class RecruitmentConfigController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ILogger<RecruitmentConfigController> _logger;
		public readonly IRecruitmentConfig _recruitmentConfig;

		public RecruitmentConfigController(ILogger<RecruitmentConfigController> logger, IConfiguration configuration, IRecruitmentConfig recruitmentConfig)
		{
			_logger = logger;
			_configuration = configuration;
			_recruitmentConfig = recruitmentConfig;
		}




		[HttpPost("Insert")]
		public async Task<IActionResult> Insert([FromBody] RecruitmentConfigDto user)
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
				var result = await _recruitmentConfig.RecruitConfig(user);
				return result;
			}
			catch (Exception)
			{
				throw;
			}

		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll([FromQuery]RecruitmentConfigDto model)
		{
			try
			{
				
				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "GetAll";

				dynamic userDetail = await _recruitmentConfig.RecruitConfig(model);
				return userDetail;

			}
			catch (Exception ex)
			{
				return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
			}
		}


		[HttpGet("Get")]
		public async Task<IActionResult> Get([FromQuery] RecruitmentConfigDto model)
		{
			try
			{
				
				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "Get";

				dynamic userDetail = await _recruitmentConfig.Get(model);

				return userDetail;

			}
			catch (Exception ex)
			{
				return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
			}
		}

		[HttpPost("Delete")]
		public async Task<IActionResult> Delete([FromBody] RecruitmentConfigDto user)
		{
			try
			{
				
				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}
				user.BaseModel.OperationType = "Delete";

				var result = await _recruitmentConfig.RecruitConfig(user);
				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

	}
}
