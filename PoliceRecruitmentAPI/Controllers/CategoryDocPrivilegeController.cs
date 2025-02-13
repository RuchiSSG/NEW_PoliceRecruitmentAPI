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
    public class CategoryDocPrivilegeController : Controller
    {
        public IConfiguration _configuration;
        private readonly ILogger<CategoryDocPrivilegeController> _logger;
        public readonly ICategoryDocPrivilegeService _categorymaster;
        public CategoryDocPrivilegeController(ILogger<CategoryDocPrivilegeController> logger, IConfiguration configuration, ICategoryDocPrivilegeService dutymaster)
        {
            _logger = logger;
            _configuration = configuration;
            _categorymaster = dutymaster;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetCategory([FromQuery] CategoryPrirvilegeDto user)
        {
            try
            {

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetAll";

                var createduser = await _categorymaster.Category(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetCategoryName")]
        public async Task<IActionResult> GetCategoryName([FromQuery] CategoryPrirvilegeDto user)
        {
            try
            {

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetCategoryName";

                var createduser = await _categorymaster.Category(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById([FromQuery] CategoryPrirvilegeDto user)
        {
            try
            {

                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "Get";

                var createduser = await _categorymaster.Category(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpGet("GetMenu")]
        public async Task<IActionResult> Getmenu([FromQuery] CategoryPrirvilegeDto user)
        {
            try
            {


                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "GetMenu";

                var createduser = await _categorymaster.Category(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromBody] CategoryPrirvilegeDto user)
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
                dataTable.Columns.Add("DocumentValidateDate", typeof(DateTime));
                //if (user.Id != null && user.Privilage == null)
                //{
                //    user.BaseModel.OperationType = "UpdateStatus";
                //}
                //else
                //{
                    foreach (var privilage in user.Privilage)
                    {
                        dataTable.Rows.Add(
                            privilage.a_menuid,
                            privilage.addaccess,
                            privilage.editaccess,
                            privilage.deleteaccess,
                            privilage.viewaccess,
                            privilage.workflow,
                            privilage.DocumentValidateDate


                        );
                    }
                    // user.Privilage = null;
                    user.DataTable = dataTable;
                //}
                var createduser = await _categorymaster.Get(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("DeleteCategory")]
        public async Task<IActionResult> DeleteDuty([FromBody] CategoryPrirvilegeDto user)
        {
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }

            user.BaseModel.OperationType = "Delete";
            var productDetails = await _categorymaster.Get(user);
            return productDetails;
        }
    }
}
