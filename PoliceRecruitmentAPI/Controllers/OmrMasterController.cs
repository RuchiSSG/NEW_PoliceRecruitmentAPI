using common;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Data;
using System.Text;

namespace PoliceRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExampleFilterAttribute]
    public class OmrMasterController : Controller
    {
        public IConfiguration _configuration;
        private readonly ILogger<OmrMasterController> _logger;
        public readonly IOmrMasterService _omrMasterService;

        public OmrMasterController(ILogger<OmrMasterController> logger, IConfiguration configuration, IOmrMasterService omrMasterService)
        {
            _logger = logger;
            _configuration = configuration;
            _omrMasterService = omrMasterService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] OmrMasterDto user)
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
                
                //user.Castsdata = null;
                //user.DataTable = dataTable;
                var result = await _omrMasterService.Get(user);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get([FromQuery] OmrMasterDto model)
        {
            try
            {
                //ShotPutDto model = new ShotPutDto();
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "Get";

                dynamic userDetail = await _omrMasterService.Get(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] string userid, [FromQuery] string recConfId, [FromQuery] string QuestionSet)
        {

            OmrMasterDto model = new OmrMasterDto();

            model.RecruitId = recConfId;
            model.UserId = userid;
            model.QuestionSet = QuestionSet;
            try
            {
                //ShotPutDto model = new ShotPutDto();
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAll";

                dynamic userDetail = await _omrMasterService.OMR(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] OmrMasterDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "Delete";

                var result = await _omrMasterService.OMR(model);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
