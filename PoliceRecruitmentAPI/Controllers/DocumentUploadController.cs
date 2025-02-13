using common;
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
	public class DocumentUploadController : ControllerBase
	{
		public IConfiguration _configuration;
		private readonly ILogger<DocumentUploadController> _logger;
		public readonly IDocumentService _docService;

		public DocumentUploadController(ILogger<DocumentUploadController> logger, IConfiguration configuration, IDocumentService docService)
		{
			_logger = logger;
			_configuration = configuration;
			_docService = docService;
		}
		[HttpPost(Name = "Upload")]
		public async Task<IActionResult> Upload([FromBody]DocumentDto model)
		{
			try
			{
				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "Insert";
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Document", typeof(string));
				dataTable.Columns.Add("DocumentName", typeof(string));
				dataTable.Columns.Add("Status", typeof(string));
                dataTable.Columns.Add("DocumentValidateDate", typeof(DateTime));
                foreach (var privilage in model.DocumentData)
				{
					dataTable.Rows.Add(
						privilage.Document,
						privilage.DocumentName,
						privilage.Status,
                        privilage.DocumentValidateDate
                    );
				}
				foreach (var privilage in model.DocumentData)
				{
					if (privilage.Status == "0")
					{
						model.Stage = "Fail";
						model.Status = "0";
						break;
					}
					else
					{
						model.Stage = "Pass";
						model.Status = "1";
					}
				}
				model.DocumentData = null;
				model.DataTable = dataTable;
				dynamic userDetail = await _docService.Document(model);
				return userDetail;

			}
			catch (Exception ex)
			{
				return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
			}
		}
		[HttpGet("Get")]
		public async Task<IActionResult> Get([FromQuery]DocumentDto model)
		{
			try
			{
				
				if (model.BaseModel == null)
				{
					model.BaseModel = new BaseModel();
				}
				model.BaseModel.OperationType = "Get";

				dynamic userDetail = await _docService.Get(model);

				return userDetail;

			}
			catch (Exception ex)
			{
				return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
			}
		}
	}
}
