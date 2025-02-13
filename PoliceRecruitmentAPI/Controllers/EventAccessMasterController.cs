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
    public class EventAccessMasterController : Controller
    {
        public IConfiguration _configuration;
        private readonly ILogger<EventAccessMasterController> _logger;
        public readonly IEventAccessService _EventAccessmaster;
        public EventAccessMasterController(ILogger<EventAccessMasterController> logger, IConfiguration configuration, IEventAccessService Eventmaster)
        {
            _logger = logger;
            _configuration = configuration;
            _EventAccessmaster = Eventmaster;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllEvent([FromQuery] EventAccessMasterDto user)
        {
            try
            {

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAll";

                var createduser = await _EventAccessmaster.Event(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetEventName")]
        public async Task<IActionResult> GetEventName([FromQuery] EventAccessMasterDto user)
        {
            try
            {

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetCategoryName";

                var createduser = await _EventAccessmaster.Event(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetEventById")]
        public async Task<IActionResult> GetEventById([FromQuery] EventAccessMasterDto user)
        {
            try
            {

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "Get";

                var createduser = await _EventAccessmaster.Event(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpGet("GetMenu")]
        public async Task<IActionResult> Getmenu([FromQuery] EventAccessMasterDto user)
        {
            try
            {


                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetMenu";

                var createduser = await _EventAccessmaster.Event(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPost("InsertEvent")]
        public async Task<IActionResult> InsertEvent([FromBody] EventAccessMasterDto user)
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
                user.Createddate = DateTime.Now;
                user.Updateddate = DateTime.Now;
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Menuid", typeof(string));
                dataTable.Columns.Add("addaccess", typeof(string));
                dataTable.Columns.Add("editaccess", typeof(string));
                dataTable.Columns.Add("deleteaccess", typeof(string));
                dataTable.Columns.Add("viewaccess", typeof(string));
                dataTable.Columns.Add("workflow", typeof(string));
                if (user.Id != null && user.Privilage == null)
                {
                    user.BaseModel.OperationType = "UpdateStatus";
                }
                else
                {
                    foreach (var privilage in user.Privilage)
                    {
                        dataTable.Rows.Add(
                            privilage.a_menuid,
                            privilage.addaccess,
                            privilage.editaccess,
                            privilage.deleteaccess,
                            privilage.viewaccess,
                            privilage.workflow
                        );
                    }
                    // user.Privilage = null;
                    user.DataTable = dataTable;
                }
                var createduser = await _EventAccessmaster.Get(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent([FromBody] EventAccessMasterDto user)
        {
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }

            user.BaseModel.OperationType = "Delete";
            var productDetails = await _EventAccessmaster.Get(user);
            return productDetails;
        }
    }
}
