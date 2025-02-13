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
				return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
			}
		}
	}
}
