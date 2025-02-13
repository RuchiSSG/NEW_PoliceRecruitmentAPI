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
	public class RecruitmentController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ILogger<RecruitmentController> _logger;
		public readonly IRecruitmentService _recruitment;

		public RecruitmentController(ILogger<RecruitmentController> logger, IConfiguration configuration, IRecruitmentService recruitment)
		{
			_logger = logger;
			_configuration = configuration;
			_recruitment = recruitment;
		}




		[HttpPost("Insert")]
		public async Task<IActionResult> Insert([FromBody] RecruitmentDto user)
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
				var result = await _recruitment.Recruit(user);
				return result;
			}
			catch (Exception)
			{
				throw;
			}

		}

		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll([FromQuery]RecruitmentDto model)
		{
			try
			{
				
				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "GetAll";

				dynamic userDetail = await _recruitment.Recruit(model);
				return userDetail;

			}
			catch (Exception ex)
			{
				return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
			}
		}


		[HttpGet("Get")]
		public async Task<IActionResult> Get([FromQuery] RecruitmentDto model)
		{
			try
			{
				
				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "Get";

				dynamic userDetail = await _recruitment.Get(model);

				return userDetail;

			}
			catch (Exception ex)
			{
				return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
			}
		}

		[HttpPost("Delete")]
		public async Task<IActionResult> Delete([FromBody] RecruitmentDto user)
		{
			try
			{
				
				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}
				user.BaseModel.OperationType = "Delete";

				var result = await _recruitment.Recruit(user);
				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

	}
}
