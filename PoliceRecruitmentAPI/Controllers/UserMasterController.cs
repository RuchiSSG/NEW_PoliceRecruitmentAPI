using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Data;
using LicenseContext = OfficeOpenXml.LicenseContext;
using DataTable = System.Data.DataTable;
using Task = System.Threading.Tasks.Task;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using common;
using System.Globalization;


namespace PoliceRecruitmentAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [ExampleFilterAttribute]
    public class UserMasterController : ControllerBase
	{

		public IConfiguration _configuration;
		private readonly ILogger<UserMasterController> _logger;
		public readonly IUserMasterService _usermaster;

		public UserMasterController(ILogger<UserMasterController> logger, IConfiguration configuration, IUserMasterService usermaster)
		{
			_logger = logger;
			_configuration = configuration;
			_usermaster = usermaster;
		}


		[HttpGet("GetAll")]
		public async Task<IActionResult> GetAll([FromQuery]UserMasterDto user)
		{
			try
			{

				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}

				user.BaseModel.OperationType = "GetAll";
				var createduser = await _usermaster.UserMaster(user);
				return createduser;
			}
            catch (Exception ex)
            {
                // Using LogErrorResponse model for cleaner code
                var errorResponse = new LogErrorResponse
                {
                    ErrorId = Guid.NewGuid().ToString("N"),
                    Timestamp = DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    OperationType = user?.BaseModel?.OperationType ?? "Unknown"
                };

                // Log error details
                _logger.LogError(ex, "{SeparatorLine}\n"+"Error ID: {ErrorId}\t" +"DateTime: {FormattedTimestamp}\n" +"Error Message: {Message}\n" +"Stack Trace: {StackTrace}\n"+"{SeparatorLine}",
                     LogErrorResponse.SEPARATOR_LINE,
                     errorResponse.ErrorId,
                     errorResponse.FormattedTimestamp,
                     errorResponse.Message,
                     errorResponse.StackTrace,
                     LogErrorResponse.SEPARATOR_LINE
                 );

                return new JsonResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

		
		[HttpGet("Get")]
		public async Task<IActionResult> Get([FromQuery] UserMasterDto user)
		{
			
			if (user.BaseModel == null)
			{
				user.BaseModel = new BaseModel();
			}
			
			user.BaseModel.OperationType = "Get";
			try
			{
				var parameter = await _usermaster.Get(user);
				return parameter;
			}
            catch (Exception ex)
            {
                // Using LogErrorResponse model for cleaner code
                var errorResponse = new LogErrorResponse
                {
                    ErrorId = Guid.NewGuid().ToString("N"),
                    Timestamp = DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    OperationType = user?.BaseModel?.OperationType ?? "Unknown"
                };

                // Log error details
                _logger.LogError(ex, "{SeparatorLine}\n"+"Error ID: {ErrorId}\t" +"DateTime: {FormattedTimestamp}\n" +"Error Message: {Message}\n" +"Stack Trace: {StackTrace}\n"+"{SeparatorLine}",
                     LogErrorResponse.SEPARATOR_LINE,
                     errorResponse.ErrorId,
                     errorResponse.FormattedTimestamp,
                     errorResponse.Message,
                     errorResponse.StackTrace,
                     LogErrorResponse.SEPARATOR_LINE
                 );

                return new JsonResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }



		[HttpPost]
		public async Task<IActionResult> Insert([FromBody] UserMasterDto user)
		{
			try
			{
				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}

				if (user.um_id == null)
				{
					user.BaseModel.OperationType = "Insert";
				}
				else
				{
					user.um_updateddate = DateTime.Now;
					user.BaseModel.OperationType = "Update";
				}
				dynamic createduser = await _usermaster.UserMaster(user);
				var outcomeidvalue = createduser.Value.Outcome.OutcomeId;
				//if (outcomeidvalue == 1)
				//{

				//	var datavalue = createduser.Value.Outcome.OutcomeDetail;

				//	await SendNo(datavalue);
				//}

				return createduser;
			}
            catch (Exception ex)
            {
                // Using LogErrorResponse model for cleaner code
                var errorResponse = new LogErrorResponse
                {
                    ErrorId = Guid.NewGuid().ToString("N"),
                    Timestamp = DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    OperationType = user?.BaseModel?.OperationType ?? "Unknown"
                };

                // Log error details
                _logger.LogError(ex, "{SeparatorLine}\n"+"Error ID: {ErrorId}\t" +"DateTime: {FormattedTimestamp}\n" +"Error Message: {Message}\n" +"Stack Trace: {StackTrace}\n"+"{SeparatorLine}",
                     LogErrorResponse.SEPARATOR_LINE,
                     errorResponse.ErrorId,
                     errorResponse.FormattedTimestamp,
                     errorResponse.Message,
                     errorResponse.StackTrace,
                     LogErrorResponse.SEPARATOR_LINE
                 );

                return new JsonResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }


		[HttpGet("Shuffle")]
		public async Task<IActionResult> Shuffle([FromQuery] UserMasterDto user)
		{
			try
			{
				

				if (user.BaseModel == null)
				{
					user.BaseModel = new BaseModel();
				}
				user.BaseModel.OperationType = "Shuffle";
				
				dynamic createduser = await _usermaster.UserMaster(user);
				var outcomeidvalue = createduser.Value.Outcome.OutcomeId;


				return createduser;
			}
            catch (Exception ex)
            {
                // Using LogErrorResponse model for cleaner code
                var errorResponse = new LogErrorResponse
                {
                    ErrorId = Guid.NewGuid().ToString("N"),
                    Timestamp = DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    OperationType = user?.BaseModel?.OperationType ?? "Unknown"
                };

                // Log error details
                _logger.LogError(ex, "{SeparatorLine}\n"+"Error ID: {ErrorId}\t" +"DateTime: {FormattedTimestamp}\n" +"Error Message: {Message}\n" +"Stack Trace: {StackTrace}\n"+"{SeparatorLine}",
                     LogErrorResponse.SEPARATOR_LINE,
                     errorResponse.ErrorId,
                     errorResponse.FormattedTimestamp,
                     errorResponse.Message,
                     errorResponse.StackTrace,
                     LogErrorResponse.SEPARATOR_LINE
                 );

                return new JsonResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
		private async Task SendNo(dynamic datavalue2)
		{

			string[] parts = datavalue2.Split(';');
			string userpassword = "";
			string username = "";
			string title = "Login Credentials";
			string email = "";
			if (parts.Length == 3)
			{
				userpassword = parts[0]; // Extract the password part
				email = parts[1];    // Extract the email part
				username = parts[2];    // Extract the email part

				// Now you can use the password and email variables as needed

			}
			string htmlContent = "<div style=\"font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;\">" +
						"<div style=\"max-width: 600px; margin: 0 auto; background-color: #fff; padding: 20px; border-radius: 5px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\">" +
							"<div style=\"text-align: center;\">" +
								"<h1 style=\"margin: 0; font-size: 28px;\">LMS</h1>" +
							"</div>" +
							"<div style=\"text-align: center; margin-top: 20px;\">" +
								"<h2 style=\"margin: 0;\">Get started</h2>" +
								"<p style=\"margin: 10px 0; font-size: 16px;\">Your account has been created on the LMS. Below are your system generated credentials.</p>" +
								"<p style=\"margin: 10px 0; font-size: 16px;\">Please use this credentials for login</p>" +
								"<div style=\"text-align: center; margin-top: 20px;\">" +
									"<p style=\"margin: 5px 0; font-size: 16px;\"><strong>Username:</strong> " + username + " </p>" +
									"<p style=\"margin: 5px 0; font-size: 16px;\"><strong>Password:</strong> " + userpassword + " </p>" +
								"</div>" +

							"</div>" +
						"</div>" +
					"</div>";

			// Split email addresses
			EmailConfigureDto user = new EmailConfigureDto();
			dynamic emailDetails = await _usermaster.GetEmailId(user);





			if (emailDetails != null)
			{
				// Use the retrieved email configuration details to send the email
				var message = new MimeMessage();
				message.From.Add(new MailboxAddress("Rensa Tubes", emailDetails.Value.Data.email)); // set your email
				message.To.Add(new MailboxAddress(null, email.Trim()));

				message.Subject = title;
				var bodyBuilder = new BodyBuilder();
				bodyBuilder.HtmlBody = htmlContent;
				message.Body = bodyBuilder.ToMessageBody();

				try
				{
					using (var client = new SmtpClient())
					{
						// Connect to the SMTP server and send the email
						client.Connect(emailDetails.Value.Data.smtp_server, emailDetails.Value.Data.smtp_port, false);
						client.Authenticate(emailDetails.Value.Data.email, emailDetails.Value.Data.password);
						client.Send(message);
						client.Disconnect(true);
					}
				}
				catch (Exception ex)
				{
					// Handle SMTP client errors
					Console.WriteLine($"Failed to send email: {ex.Message}");
				}
			}
			else
			{
				// Handle case where email configuration details are not found
				Console.WriteLine("Email configuration details not found.");
			}

			

		}



		[HttpPost("Delete")]
		public async Task<IActionResult> Delete([FromBody] UserMasterDto user)
		{
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }

                user.BaseModel.OperationType = "Delete";
                var usertDetails = await _usermaster.UserMaster(user);
                return usertDetails;
            }
            catch (Exception ex)
            {
                // Using LogErrorResponse model for cleaner code
                var errorResponse = new LogErrorResponse
                {
                    ErrorId = Guid.NewGuid().ToString("N"),
                    Timestamp = DateTime.Now,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    OperationType = user?.BaseModel?.OperationType ?? "Unknown"
                };

                // Log error details
                _logger.LogError(ex, "{SeparatorLine}\n"+"Error ID: {ErrorId}\t" +"DateTime: {FormattedTimestamp}\n" +"Error Message: {Message}\n" +"Stack Trace: {StackTrace}\n"+"{SeparatorLine}",
                     LogErrorResponse.SEPARATOR_LINE,
                     errorResponse.ErrorId,
                     errorResponse.FormattedTimestamp,
                     errorResponse.Message,
                     errorResponse.StackTrace,
                     LogErrorResponse.SEPARATOR_LINE
                 );

                return new JsonResult(errorResponse)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    




        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadExcel([FromForm] IFormFile file)
        //{
        //	if (file == null || file.Length == 0)
        //	{
        //		return BadRequest("No file uploaded.");
        //	}

        //	// Define the path where the file will be saved temporarily
        //	var filePath = Path.GetTempFileName();

        //	try
        //	{
        //		// Save the uploaded file to the temporary path
        //		using (var stream = new FileStream(filePath, FileMode.Create))
        //		{
        //			await file.CopyToAsync(stream);
        //		}

        //		// Initialize Excel Interop objects
        //		Application excel = new Application();
        //		Workbook wb = excel.Workbooks.Open(filePath);
        //		Worksheet ws = (Worksheet)wb.Sheets[1]; // Assuming the first sheet is the one you want to read

        //		// Read data from Excel
        //		int rowCount = ws.UsedRange.Rows.Count;
        //		var UserDataList = new List<UserMasterDto>();

        //		for (int row = 2; row <= rowCount; row++) // assuming first row is header
        //		{
        //			var newData = new UserMasterDto
        //			{
        //				um_user_name = ws.Cells[row, 1].ToString(),
        //				um_staffname = ws.Cells[row, 2].ToString(),
        //				// Map other properties similarly
        //			};
        //			UserDataList.Add(newData);
        //		}

        //		// Clean up Excel Interop objects
        //		wb.Close(false);
        //		excel.Quit();
        //		System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);

        //		// Optionally, delete the temporary file
        //		System.IO.File.Delete(filePath);

        //		// Save to database or further processing

        //		return Ok("Data uploaded successfully.");
        //	}
        //	catch (Exception ex)
        //	{
        //		// Clean up Excel Interop objects in case of exception
        //		try
        //		{
        //			// Close and quit Excel application
        //			//excel.Quit();
        //			//System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
        //		}
        //		catch { } // Ignore any exception during cleanup

        //		// Optionally, delete the temporary file
        //		System.IO.File.Delete(filePath);

        //		return StatusCode(500, $"Error uploading file: {ex.Message}");
        //	}
        //}
        public static class FileConverter
        {
            public static void ConvertCsvToXlsx(Stream inputStream, Stream outputStream)
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    using (var reader = new StreamReader(inputStream))
                    {
                        int row = 1;
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            var values = line.Split(',');

                            for (int col = 0; col < values.Length; col++)
                            {
                                worksheet.Cells[row, col + 1].Value = values[col];
                            }

                            row++;
                        }
                    }

                    package.SaveAs(outputStream);
                }
            }

            public static void ConvertXlsToXlsx(Stream inputStream, Stream outputStream)
            {
                using (var spreadsheetDocument = SpreadsheetDocument.Open(inputStream, false))
                {
                    var workbookPart = spreadsheetDocument.WorkbookPart;
                    using (var memoryStream = new MemoryStream())
                    {
                        var newSpreadsheetDocument = SpreadsheetDocument.Create(memoryStream, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook);
                        var newWorkbookPart = newSpreadsheetDocument.AddWorkbookPart();
                        newWorkbookPart.Workbook = new Workbook();
                        newWorkbookPart.Workbook.Sheets = new Sheets();

                        uint sheetId = 1;
                        foreach (var sheet in workbookPart.Workbook.Sheets.OfType<Sheet>())
                        {
                            var oldSheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                            var newSheetPart = newWorkbookPart.AddNewPart<WorksheetPart>();

                            newSheetPart.FeedData(oldSheetPart.GetStream());
                            var newSheet = new Sheet { Id = newWorkbookPart.GetIdOfPart(newSheetPart), SheetId = sheetId++, Name = sheet.Name };
                            newWorkbookPart.Workbook.Sheets.Append(newSheet);
                        }

                        newWorkbookPart.Workbook.Save();
                        newSpreadsheetDocument.Clone();

                        memoryStream.Position = 0;
                        memoryStream.CopyTo(outputStream);
                    }
                }
            }
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcel(IFormFile file, [FromForm] string userId, [FromForm] string um_recruitid)
        {
            
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            UserMasterDto user = new UserMasterDto { BaseModel = new BaseModel { OperationType = "InsertData" } };
            user.UserId = userId;
            user.um_recruitid = um_recruitid;
            if (file == null || file.Length == 0)
            {
                return Ok(new Outcome { OutcomeId = 0, OutcomeDetail = "No data in the excel!" });
            }

            string[] allowedFileExtensions = { ".xls", ".xlsx", ".xlsm", ".csv" };
            if (!allowedFileExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                ModelState.AddModelError("File", "Please upload a file of type: " + string.Join(", ", allowedFileExtensions));
                return BadRequest(ModelState);
            }

            DataTable dataTable = new DataTable();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                MemoryStream convertedStream = new MemoryStream();
                if (Path.GetExtension(file.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    FileConverter.ConvertCsvToXlsx(stream, convertedStream);
                }
                else if (Path.GetExtension(file.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase))
                {
                    FileConverter.ConvertXlsToXlsx(stream, convertedStream);
                }

                MemoryStream newStream = convertedStream.Length > 0 ? convertedStream : stream;
                newStream.Position = 0;

                using (var package = new ExcelPackage(newStream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    if (rowCount == 1)
                    {
                        return Ok(new Outcome { OutcomeId = 0, OutcomeDetail = "No data in the excel!" });
                    }

                    // Adding columns to DataTable based on Excel header row (first row)
                    for (int col = 1; col <= colCount; col++)
                    {
                        string columnName = worksheet.Cells[1, col].Value?.ToString();
                        if (!string.IsNullOrEmpty(columnName))
                        {
                            dataTable.Columns.Add(new DataColumn(columnName, typeof(string)));
                        }
                    }

                    // Adding rows to DataTable from Excel data
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var dataRow = dataTable.NewRow();
                        for (int col = 1; col <= colCount; col++)
                        {
                            dataRow[col - 1] = worksheet.Cells[row, col].Value?.ToString();
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }

            user.DataTable = dataTable;
            var parameter = await _usermaster.UserMaster(user);
            return parameter;
        }

        [HttpGet("download")]
        public IActionResult DownloadExcel()
        {
            // Define the column names for the Excel file
            string[] columnNames = { "um_user_name",  "um_staffname", "um_bukkel_no", "um_post", "um_phone_no" };

            // Set EPPlus LicenseContext to NonCommercial (or Commercial if applicable)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Add the column names to the first row
                for (int col = 0; col < columnNames.Length; col++)
                {
                    worksheet.Cells[1, col + 1].Value = columnNames[col];
                }

                // Save the package to a memory stream
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                // Define the file name
                string excelName = $"Template-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

                // Return the file as a downloadable response
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

    }
}
