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
using CsvHelper.Configuration;
using CsvHelper;
using PoliceRecruitmentAPI.Services.ApiServices;
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
        public async Task<IActionResult> GetAll([FromQuery] UserMasterDto user)
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
                throw;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }

            user.BaseModel.OperationType = "Delete";
            var usertDetails = await _usermaster.UserMaster(user);
            return usertDetails;
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
            UserMasterDto user = new UserMasterDto();
            user.UserId = userId;
            user.um_recruitid = um_recruitid;
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }



            if (file == null || file.Length == 0)
            {
                return StatusCode(422, new Outcome { OutcomeId = 3, OutcomeDetail = "No data in the excel!" });

                //return Ok(new Outcome { OutcomeId = 3, OutcomeDetail = "No data in the excel!" });
            }

            string[] allowedFileExtensions = { ".xls", ".xlsx", ".xlsm", ".csv" };
            if (!allowedFileExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                ModelState.AddModelError("File", "Please upload a file of type: " + string.Join(", ", allowedFileExtensions));
                return BadRequest(ModelState);
            }

            DataTable dataTable = new DataTable();
            List<string> errorLog = new List<string>();
            HashSet<string> usernamesSeen = new HashSet<string>();  // Track seen usernames
            HashSet<string> BukkelNosSeen = new HashSet<string>();
            HashSet<string> DutynameSeen = new HashSet<string>();

            user.BaseModel.OperationType = "ExistingCandidateUserName";
            dynamic existingUsernames1 = await _usermaster.UserMaster(user);

            // Manually loop through the dynamic data and extract the usernames
            HashSet<string> existingUsernames = new HashSet<string>();
            foreach (var userObj in existingUsernames1.Value.Data)
            {
                string username = userObj.um_user_name?.ToString().Trim();
                if (!string.IsNullOrEmpty(username))
                {
                    existingUsernames.Add(username);
                }
            }

            // Do the same for tokenNos and applicationNos
            user.BaseModel.OperationType = "ExistingCandidateBukkelNo";
            dynamic existingBukkelNo1 = await _usermaster.UserMaster(user);

            HashSet<string> existingBukkelNos = new HashSet<string>();
            foreach (var BukkelNoObj in existingBukkelNo1.Value.Data)
            {
                string BukkelNo = BukkelNoObj.um_bukkel_no?.ToString().Trim();
                if (!string.IsNullOrEmpty(BukkelNo))
                {
                    existingBukkelNos.Add(BukkelNo);
                }
            }

            user.BaseModel.OperationType = "ExistingDutyName";
            dynamic existingDutyname1 = await _usermaster.UserMaster(user);

            HashSet<string> existingDutyname = new HashSet<string>();
            foreach (var DutynameObj in existingDutyname1.Value.Data)
            {
                string Dutyname = DutynameObj.d_dutyname?.ToString().Trim();
                if (!string.IsNullOrEmpty(Dutyname))
                {
                    existingDutyname.Add(Dutyname);
                }
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                // Handling CSV separately
                if (Path.GetExtension(file.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    using (var reader = new StreamReader(stream))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        // Make sure CsvHelper knows to handle quoted values properly
                        Quote = '"',
                        Delimiter = ",",
                        BadDataFound = null // To ignore invalid data rows
                    }))
                    {
                        var records = csv.GetRecords<dynamic>().ToList();

                        // Creating DataTable based on CSV data
                        if (records.Any())
                        {
                            // Add columns to DataTable
                            var header = ((IDictionary<string, object>)records[0]).Keys.ToList();
                            foreach (var column in header)
                            {
                                dataTable.Columns.Add(new DataColumn(column, typeof(string)));
                            }

                            // Add rows to DataTable
                            foreach (var record in records)
                            {
                                var dataRow = dataTable.NewRow();
                                foreach (var column in header)
                                {
                                    dataRow[column] = ((IDictionary<string, object>)record)[column]?.ToString();
                                }

                                string username = dataRow["User Name"]?.ToString()?.Trim();
                                if (string.IsNullOrEmpty(username))
                                {
                                    errorLog.Add($"BlankUserName :{username}");
                                }
                                else
                                if (usernamesSeen.Contains(username) || existingUsernames.Contains(username))
                                {
                                    errorLog.Add($"DuplicateUsername : {username}");
                                }
                                else
                                {
                                    usernamesSeen.Add(username);
                                }

                                string BukkelNo = dataRow["Bukkel No"]?.ToString()?.Trim();
                                if (string.IsNullOrEmpty(BukkelNo))
                                {
                                    errorLog.Add($"BlankBukkelNo :{BukkelNo}");
                                }
                                else
                                if (BukkelNosSeen.Contains(BukkelNo) || existingBukkelNos.Contains(BukkelNo))
                                {
                                    errorLog.Add($"DuplicateBukkelNo : {BukkelNo}");
                                }
                                else
                                {
                                    BukkelNosSeen.Add(BukkelNo);
                                }
                                string DutyNames = dataRow["Duty"]?.ToString()?.Trim();
                                if (string.IsNullOrEmpty(DutyNames))
                                {
                                    errorLog.Add($"BlankDutyName :{DutyNames}");
                                }
                                else
                                if (DutynameSeen.Contains(DutyNames) || existingDutyname.Contains(DutyNames))
                                {
                                    DutynameSeen.Add(DutyNames);
                                   
                                }
                                else
                                {
                                    errorLog.Add($"DutyNotAvailable : {DutyNames}");
                                }

                                dataTable.Rows.Add(dataRow);
                            }
                        }
                        else
                        {
                            return StatusCode(422, new Outcome { OutcomeId = 3, OutcomeDetail = "No data in the excel!" });

                            //return Ok(new Outcome { OutcomeId = 3, OutcomeDetail = "No data in the excel!" });

                        }
                    }
                }
                else
                {
                    // If it’s an Excel file (XLS, XLSX), use EPPlus to convert and process
                    MemoryStream convertedStream = new MemoryStream();
                    if (Path.GetExtension(file.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase))
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
                            return StatusCode(422, new Outcome { OutcomeId = 3, OutcomeDetail = "No data in the excel!" });

                            //return Ok(new Outcome { OutcomeId = 3, OutcomeDetail = "No data in the excel!" });
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

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var dataRow = dataTable.NewRow();
                            for (int col = 1; col <= colCount; col++)
                            {
                                dataRow[col - 1] = worksheet.Cells[row, col].Value?.ToString();
                            }

                            string username = dataRow["User Name"]?.ToString()?.Trim();
                            if (string.IsNullOrEmpty(username))
                            {
                                errorLog.Add($"BlankUserName :{username}");
                            }
                            else
                            if (usernamesSeen.Contains(username) || existingUsernames.Contains(username))
                            {
                                errorLog.Add($"DuplicateUsername: {username}");  //
                            }
                            else
                            {
                                usernamesSeen.Add(username);
                            }

                            string BukkelNo = dataRow["Bukkel No"]?.ToString()?.Trim();
                            if (string.IsNullOrEmpty(BukkelNo))
                            {
                                errorLog.Add($"BlankBukkelNo :{BukkelNo}");
                            }
                            else
                           if (BukkelNosSeen.Contains(BukkelNo) || existingBukkelNos.Contains(BukkelNo))
                            {
                                errorLog.Add($"DuplicateBukkelNo: {BukkelNo}");
                            }
                            else
                            {
                                BukkelNosSeen.Add(BukkelNo);
                            }
                            string DutyNames = dataRow["Duty"]?.ToString()?.Trim();
                            if (string.IsNullOrEmpty(DutyNames))
                            {
                                errorLog.Add($"BlankDutyName :{DutyNames}");
                            }
                            else
                            if (DutynameSeen.Contains(DutyNames) || existingDutyname.Contains(DutyNames))
                            {
                                DutynameSeen.Add(DutyNames);

                            }
                            else
                            {
                                errorLog.Add($"DutyNotAvailable : {DutyNames}");
                            }

                            dataTable.Rows.Add(dataRow);
                        }
                    }
                }
            }
            var duplicateUsername = errorLog.Where(e => e.Contains("DuplicateUsername")).ToList();
            var duplicateBukkelNo = errorLog.Where(e => e.Contains("DuplicateBukkelNo")).ToList();
            var blankUserName = errorLog.Where(e => e.Contains("BlankUserName")).ToList();
            var blankBukkelNo = errorLog.Where(e => e.Contains("BlankBukkelNo")).ToList();
            var dutynames = errorLog.Where(e => e.Contains("DutyNotAvailable")).ToList();

            if (errorLog.Any())
            {
                var response = new
                {
                    OutcomeId = 4,
                    DuplicateUsernameErrors = duplicateUsername,
                    DuplicateBukkelNoErrors = duplicateBukkelNo,
                    BlankUserNameErrors = blankUserName,
                    BlankBukkelNoErrors = blankBukkelNo,
                    DutyNotAvailable= dutynames
                };
                return StatusCode(409, response);
                //return StatusCode(409, new Outcome { OutcomeId = 4, OutcomeDetail = string.Join("; ", errorLog) });

            }
            user.BaseModel.OperationType = "InsertData";
            user.DataTable = dataTable;
            var parameter = await _usermaster.UserMaster(user);
            return parameter;
        }

        //public async Task<IActionResult> UploadExcel(IFormFile file, [FromForm] string userId, [FromForm] string um_recruitid)
        //{


        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    UserMasterDto user = new UserMasterDto { BaseModel = new BaseModel { OperationType = "InsertData" } };
        //    user.UserId = userId;
        //    user.um_recruitid = um_recruitid;
        //    if (file == null || file.Length == 0)
        //    {
        //        return Ok(new Outcome { OutcomeId = 3, OutcomeDetail = "No data in the excel!" });
        //    }

        //    string[] allowedFileExtensions = { ".xls", ".xlsx", ".xlsm", ".csv" };
        //    if (!allowedFileExtensions.Contains(Path.GetExtension(file.FileName)))
        //    {
        //        ModelState.AddModelError("File", "Please upload a file of type: " + string.Join(", ", allowedFileExtensions));
        //        return BadRequest(ModelState);
        //    }

        //    DataTable dataTable = new DataTable();

        //    using (var stream = new MemoryStream())
        //    {
        //        await file.CopyToAsync(stream);
        //        stream.Position = 0;

        //        MemoryStream convertedStream = new MemoryStream();
        //        if (Path.GetExtension(file.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
        //        {
        //            FileConverter.ConvertCsvToXlsx(stream, convertedStream);
        //        }
        //        else if (Path.GetExtension(file.FileName).Equals(".xls", StringComparison.OrdinalIgnoreCase))
        //        {
        //            FileConverter.ConvertXlsToXlsx(stream, convertedStream);
        //        }

        //        MemoryStream newStream = convertedStream.Length > 0 ? convertedStream : stream;
        //        newStream.Position = 0;

        //        using (var package = new ExcelPackage(newStream))
        //        {
        //            var worksheet = package.Workbook.Worksheets[0];
        //            int rowCount = worksheet.Dimension.Rows;
        //            int colCount = worksheet.Dimension.Columns;

        //            if (rowCount == 1)
        //            {
        //               return StatusCode(422, new Outcome { OutcomeId = 3, OutcomeDetail = "No data in the excel!" });

        //            }

        //            // Adding columns to DataTable based on Excel header row (first row)
        //            for (int col = 1; col <= colCount; col++)
        //            {
        //                string columnName = worksheet.Cells[1, col].Value?.ToString();
        //                if (!string.IsNullOrEmpty(columnName))
        //                {
        //                    dataTable.Columns.Add(new DataColumn(columnName, typeof(string)));
        //                }
        //            }

        //            // Adding rows to DataTable from Excel data
        //            for (int row = 2; row <= rowCount; row++)
        //            {
        //                var dataRow = dataTable.NewRow();
        //                for (int col = 1; col <= colCount; col++)
        //                {
        //                    dataRow[col - 1] = worksheet.Cells[row, col].Value?.ToString();
        //                }
        //                dataTable.Rows.Add(dataRow);
        //            }
        //        }
        //    }

        //    user.DataTable = dataTable;
        //    var parameter = await _usermaster.UserMaster(user);
        //    return parameter;
        //}

        [HttpGet("download")]
        public IActionResult DownloadExcel()
        {
            // Define the column names for the Excel file
            string[] columnNames = { "um_user_name", "um_staffname", "um_bukkel_no", "um_post", "um_phone_no" };

            // Set EPPlus LicenseContext to NonCommercial (or Commercial if applicable)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;


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
