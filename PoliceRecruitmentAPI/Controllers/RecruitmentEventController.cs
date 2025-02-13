using common;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.ApiServices;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Data;

namespace PoliceRecruitmentAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[ExampleFilterAttribute]
	public class RecruitmentEventController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ILogger<RecruitmentEventController> _logger;
		public readonly IRecruitmentEventService _recruitmentEventService;

		public RecruitmentEventController(ILogger<RecruitmentEventController> logger, IConfiguration configuration, IRecruitmentEventService recruitmentEventService)
		{
			_logger = logger;
			_configuration = configuration;
			_recruitmentEventService = recruitmentEventService;
		}
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] RecruitmentEventDto user)
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
                //DataTable dataTable = new DataTable();
                //dataTable.Columns.Add("minValue", typeof(string));
                //dataTable.Columns.Add("maxValue", typeof(string));
                //dataTable.Columns.Add("score", typeof(decimal));
                //dataTable.Columns.Add("gender", typeof(string));
                //dataTable.Columns.Add("category", typeof(string));


                //foreach (var privilage in user.RecruitmentConfig)
                //{
                //    dataTable.Rows.Add(
                //        privilage.minValue,
                //        privilage.maxValue,
                //        privilage.score,
                //        privilage.gender,
                //        privilage.category

                //    );
                //}
                //user.RecruitmentConfig = null;
                //user.DataTable = dataTable;
                var result = await _recruitmentEventService.Get(user);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpPost("InsertEventParameter")]
        public async Task<IActionResult> InsertEventParameter([FromBody] RecruitmentEventDto user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                if (user.Id == null)
                {
                    user.BaseModel.OperationType = "InsertEventParameter";
                }
                else
                {
                    user.BaseModel.OperationType = "UpdateEventParameter";
                }
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("minValue", typeof(string));
                dataTable.Columns.Add("maxValue", typeof(string));
                dataTable.Columns.Add("score", typeof(string));
                dataTable.Columns.Add("gender", typeof(string));
                dataTable.Columns.Add("category", typeof(string));


                foreach (var privilage in user.RecruitmentConfig)
                {
                    dataTable.Rows.Add(
                        privilage.minValue,
                        privilage.maxValue,
                        privilage.score,
                        privilage.gender,
                        privilage.category

                    );
                }
                user.RecruitmentConfig = null;
                user.DataTable = dataTable;
                var result = await _recruitmentEventService.Get(user);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }


        [HttpGet("GetAllRecruit")]
		public async Task<IActionResult> GetAllRecruit([FromQuery]RecruitmentEventDto model)
		{
			try
			{
				

				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "GetAllRecruit";

				dynamic userDetail = await _recruitmentEventService.RecruitEvent(model);

				return userDetail;

			}
			catch (Exception ex)
			{
				return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
			}
		}

        
            [HttpGet("GetAllEvent")]
        public async Task<IActionResult> GetAllEvent([FromQuery] string userid, [FromQuery] string recruitid, string categoryid,string eventUnit)
        {
            try
            {
                RecruitmentEventDto model = new RecruitmentEventDto();
                model.UserId = userid;
                model.recConfId = recruitid;
                model.categoryId = categoryid.ToString();
                model.eventUnit = eventUnit;
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAllEvent";

                dynamic userDetail = await _recruitmentEventService.RecruitEvent(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpGet("GetAllRecruitEvent")]
        public async Task<IActionResult> GetAllRecruitEvent([FromQuery] string userid, [FromQuery] string recruitid,string sessionid, string ipaddress)//, [FromQuery] string categoryid = null)
        {
            try
            {
                if (sessionid == null)
                {
                    sessionid = "null";
                    ipaddress= "null";
                }

                RecruitmentEventDto model = new RecruitmentEventDto
                {
                    UserId = userid,
                    recConfId = recruitid,
                    sessionid=sessionid,
                    ipaddress=ipaddress,

                    // categoryId = categoryid,
                    BaseModel = new BaseModel
                    {
                        OperationType = "GetAllRecruitEvent"
                    }
                };

                 

                dynamic userDetail = await _recruitmentEventService.RecruitEvent(model);

                return userDetail;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in GetAllRecruitEvent");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing your request." });
            }
        }


        [HttpGet("GetAllRecruitEvent1")]
        public async Task<IActionResult> GetAllRecruitEvent1([FromQuery] string userid, [FromQuery] string recruitid, string categoryid)
        {
            try
            {
                RecruitmentEventDto model = new RecruitmentEventDto();
                model.UserId = userid;
                model.recConfId = recruitid;
                model.categoryId = categoryid.ToString();
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAllRecruitEvent1";

                dynamic userDetail = await _recruitmentEventService.RecruitEvent(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }


        [HttpGet("GetRecruit")]
		public async Task<IActionResult> GetRecruit([FromQuery] RecruitmentEventDto model)
		{
			try
			{
				
				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "GetRecruit";

				dynamic userDetail = await _recruitmentEventService.Get(model);

				return userDetail;

			}
			catch (Exception ex)
			{
				return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
			}
		}

		[HttpPost("Delete")]
		public async Task<IActionResult> Delete([FromBody] RecruitmentEventDto model)
		{
			try
			{
				
				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "Delete";

				var result = await _recruitmentEventService.RecruitEvent(model);
				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}

        [HttpPost("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent([FromBody] RecruitmentEventDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "DeleteEvent";

                var result = await _recruitmentEventService.RecruitEvent(model);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetAllEventSignature")]
        public async Task<IActionResult> GetAllEventSignature([FromQuery]  string userId, [FromQuery] string RecruitId, [FromQuery]  string groupid)
        {
            try
            {
                RecruitmentEventDto model=new RecruitmentEventDto();
                model.UserId=userId;
                model.recConfId = RecruitId;
                model.groupid=groupid;

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAllEventSignature";

                dynamic userDetail = await _recruitmentEventService.RecruitEvent(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

    
        [HttpPost("InsertSignatures")]
        public async Task<IActionResult> InsertSignatures([FromBody] AllEventSignDto user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "InsertSignatures";
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ChestNo", typeof(string));
                dataTable.Columns.Add("Signature", typeof(string));


                foreach (var privilage in user.runningData)
                {
                    dataTable.Rows.Add(
                        privilage.ChestNo,
                        privilage.Signature


                    );
                }
                user.runningData = null;
                user.DataTablesign = dataTable;
                var result = await _recruitmentEventService.GetSign(user);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
