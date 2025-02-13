using common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Data;

namespace PoliceRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExampleFilterAttribute]
    public class CandidateScheduleMasterController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ILogger<CandidateScheduleMasterController> _logger;
        public readonly ICandidateScheduleMasterService _candidateService;

        public CandidateScheduleMasterController(ILogger<CandidateScheduleMasterController> logger, IConfiguration configuration, ICandidateScheduleMasterService candidateService)
        {
            _logger = logger;
            _configuration = configuration;
            _candidateService = candidateService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] CandidateScheduleMasterDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAll";

                dynamic userDetail = await _candidateService.CandidateSchedule(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpGet("CandidateScheduleInsert")]
        public async Task<IActionResult> CandidateScheduleInsert([FromQuery] CandidateScheduleMasterDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "CandidateScheduleInsert";

                dynamic userDetail = await _candidateService.CandidateSchedule(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }


        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] CandidateScheduleMasterDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "Get";

                dynamic userDetail = await _candidateService.CandidateSchedule(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
        [HttpGet("GetScheduleCandidate")]
        public async Task<IActionResult> GetScheduleCandidate([FromQuery] CandidateScheduleMasterDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetScheduleCandidate";

                dynamic userDetail = await _candidateService.CandidateSchedule(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpGet("GetAllScheduleCandidate")]
        public async Task<IActionResult> GetAllScheduleCandidate([FromQuery] CandidateScheduleMasterDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAllScheduleCandidate";

                dynamic userDetail = await _candidateService.CandidateSchedule(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpPost("UpdateScheduleCandidate")]
        public async Task<IActionResult> UpdateScheduleCandidate([FromBody] CandidateScheduleMasterDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "UpdateScheduleCandidate";

                dynamic userDetail = await _candidateService.Get(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        //[HttpGet("GetGroup100")]
        //public async Task<IActionResult> GetGroup100([FromQuery] CandidateScheduleMasterDto model)
        //{
        //    try
        //    {

        //        if (model.BaseModel == null)
        //        {
        //            model.BaseModel = new BaseModel();
        //        }
        //        model.BaseModel.OperationType = "GetGroup";

        //        dynamic userDetail = await _candidateService.Get(model);

        //        return userDetail;

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        //    }
        //}
        //[HttpPost("Insert")]
        //public async Task<IActionResult> Insert([FromBody] CandidateScheduleMasterDto user)
        //{
        //    try
        //    {
        //        if (user.BaseModel == null)
        //        {
        //            user.BaseModel = new BaseModel();
        //        }
        //        user.BaseModel.OperationType = "Insert";
        //        DataTable dataTable = new DataTable();
        //        dataTable.Columns.Add("ChestNo", typeof(string));
        //        dataTable.Columns.Add("CandidateId", typeof(string));
        //        dataTable.Columns.Add("Group", typeof(string));
        //        dataTable.Columns.Add("StartTime", typeof(string));
        //        dataTable.Columns.Add("EndTime", typeof(string));
        //        dataTable.Columns.Add("Duration", typeof(string));
        //        dataTable.Columns.Add("distance1", typeof(string));
        //        dataTable.Columns.Add("distance2", typeof(string));
        //        dataTable.Columns.Add("distance3", typeof(string));
        //        dataTable.Columns.Add("Signature", typeof(string));
        //        dataTable.Columns.Add("Date", typeof(DateTime));
        //        dataTable.Columns.Add("Eventid", typeof(string));
        //        dataTable.Columns.Add("GrpLdrSignature", typeof(string));
        //        dataTable.Columns.Add("InchargeSignature", typeof(string));

        //        foreach (var privilage in user.runningData)
        //        {
        //            dataTable.Rows.Add(
        //                privilage.ChestNo,
        //                privilage.CandidateId,
        //                privilage.Group,
        //                privilage.StartTime,
        //                privilage.EndTime,
        //                privilage.Duration,
        //                privilage.distance1,
        //                privilage.distance2,
        //                privilage.distance3,
        //                privilage.Signature,
        //                privilage.Date,
        //                privilage.Eventid,
        //                privilage.GrpLdrSignature,
        //                privilage.InchargeSignature
        //            );
        //        }
        //        user.runningData = null;
        //        user.DataTable = dataTable;
        //        var result = await _candidateService.Running(user);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] CandidateScheduleMasterDto user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "Update";
                var result = await _candidateService.CandidateSchedule(user);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }
        //[HttpPost("UpdateSign100")]
        //public async Task<IActionResult> UpdateSign([FromBody] CandidateScheduleMasterDto user)
        //{
        //    try
        //    {
        //        if (user.BaseModel == null)
        //        {
        //            user.BaseModel = new BaseModel();
        //        }
        //        user.BaseModel.OperationType = "UpdateSign";
        //        var result = await _candidateService.Running(user);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
    }
}
