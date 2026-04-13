using common;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.ApiServices;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Data;
using System.Text;

namespace PoliceRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExampleFilterAttribute]
    public class RFIDChestNoMappingController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ILogger<RFIDChestNoMappingController> _logger;
        public readonly IRFIDChestNoMappingService _candidateService;

        public RFIDChestNoMappingController(ILogger<RFIDChestNoMappingController> logger, IConfiguration configuration, IRFIDChestNoMappingService candidateService)
        {
            _logger = logger;
            _configuration = configuration;
            _candidateService = candidateService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetCard([FromQuery] RFIDChestNoMappingDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "Get";

                dynamic userDetail = await _candidateService.RFIDChestNoMapping(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }


        [HttpGet("GetMapCandidate")]
        public async Task<IActionResult> GetMapCandidate([FromQuery] RFIDChestNoMappingDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetMappCandidate";

                dynamic userDetail = await _candidateService.RFIDChestNoMapping(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
        [HttpPost("devicerelese")]
        public async Task<IActionResult> devicerelese([FromBody] RFIDChestNoMappingDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "devicerelese";

                dynamic userDetail = await _candidateService.RFIDChestNoMapping(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
        //[HttpGet("GetChestno")]
        //public async Task<IActionResult> GetChestno([FromQuery] RFIDChestNoMappingDto model)
        //{
        //    try
        //    {

        //        if (model.BaseModel == null)
        //        {
        //            model.BaseModel = new BaseModel();
        //        }
        //        model.BaseModel.OperationType = "GetchestBarcode";

        //        dynamic userDetail = await _candidateService.RFIDChestNoMapping(model);

        //        return userDetail;

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        //    }
        //}

        [HttpGet("GetChestno")]
        public async Task<IActionResult> GetChestno([FromQuery] string userid, [FromQuery] string recruitid, [FromQuery] string eventId, [FromQuery] string eventName, [FromQuery] string sessionid, [FromQuery] string ipaddress)
        {

            //ShotPutDto model = new ShotPutDto();
            RFIDChestNoMappingDto model = null;
            try
            {
                model = new RFIDChestNoMappingDto
                {
                    UserId = userid,
                    RecruitId=recruitid,
                    eventId=eventId,
                    eventName=eventName,
                    sessionid=sessionid,
                    ipaddress = ipaddress,
                    BaseModel = new BaseModel
                    {
                        OperationType = "GetchestBarcode"
                    }
                };


                dynamic userDetail = await _candidateService.RFIDChestNoMapping(model);
                return userDetail;

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
                    OperationType = model?.BaseModel?.OperationType ?? "Unknown"
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
        //[HttpPost("Insert")]
        //public async Task<IActionResult> Insert([FromBody] RFIDChestNoMappingDto user)
        //{
        //    try
        //    {
        //        if (user.BaseModel == null)
        //        {
        //            user.BaseModel = new BaseModel();
        //        }
        //        user.BaseModel.OperationType = "Insert";
        //        user.CreatedDate= DateTime.Now;
        //        var result = await _candidateService.Get(user);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
        [HttpGet("Getgroup")]
        public async Task<IActionResult> Getgroup([FromQuery] string userid, [FromQuery] string recruitid, [FromQuery] string eventId, [FromQuery] string eventName, [FromQuery] string sessionid, [FromQuery] string ipaddress)
        {

            //ShotPutDto model = new ShotPutDto();
            RFIDChestNoMappingDto model = null;
            try
            {
                model = new RFIDChestNoMappingDto
                {
                    UserId = userid,
                    RecruitId=recruitid,
                    eventId=eventId,
                    eventName=eventName,
                    sessionid=sessionid,
                    ipaddress = ipaddress,
                    BaseModel = new BaseModel
                    {
                        OperationType = "RFIDGetGroup"
                    }
                };


                dynamic userDetail = await _candidateService.RFIDChestNoMapping(model);
                return userDetail;

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
                    OperationType = model?.BaseModel?.OperationType ?? "Unknown"
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

        [HttpGet("GetChestRFID")]
        public async Task<IActionResult> GetChestRFID([FromQuery] string userid, [FromQuery] string recruitid, [FromQuery] string eventId, [FromQuery] string eventName, [FromQuery] string sessionid, [FromQuery] string ipaddress, [FromQuery] int groupId)
        {

            //ShotPutDto model = new ShotPutDto();
            RFIDChestNoMappingDto model = null;
            try
            {
                model = new RFIDChestNoMappingDto
                {
                    UserId = userid,
                    RecruitId=recruitid,
                    eventId=eventId,
                    groupid=groupId,
                    eventName=eventName,
                    sessionid=sessionid,
                    ipaddress = ipaddress,
                    BaseModel = new BaseModel
                    {
                        OperationType = "GetAllChestNoRFID"
                    }
                };


                dynamic userDetail = await _candidateService.RFIDChestNoMapping(model);
                return userDetail;

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
                    OperationType = model?.BaseModel?.OperationType ?? "Unknown"
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

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] RFIDChestNoMappingDto user)
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
                    user.BaseModel.OperationType = "updateRFID";
                }
                user.CreatedDate= DateTime.Now;
                var createduser = await _candidateService.Get(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("InsertRIFDRunning")]
        public async Task<IActionResult> InsertRIFDRunning([FromBody] RFIDChestNoMappingDto user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "InsertRIFDRunning";
                user.CreatedDate = DateTime.Now;
                var result = await _candidateService.GetRFID(user);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody] RFIDChestNoMappingDto user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "Delete";
                user.CreatedDate = DateTime.Now;
                var result = await _candidateService.Get(user);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost("DeleteAllMapping")]
        public async Task<IActionResult> DeleteAllMapping([FromBody] RFIDChestNoMappingDto user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "DeleteAllMapping";
                user.CreatedDate = DateTime.Now;
                var result = await _candidateService.Get(user);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPost("RIFDRunningDelete")]
        public async Task<IActionResult> RIFDRunningDelete([FromQuery] string userid, [FromQuery] string recruitid, [FromQuery] string deviceid, [FromQuery] string Location, [FromQuery] string eventName, [FromQuery] string eventId, [FromQuery] string sessionid, [FromQuery] string ipaddress)
        {
            try
            {
                RFIDChestNoMappingDto user = new RFIDChestNoMappingDto();
                user.UserId = userid;
                user.RecruitId = recruitid;
                user.DeviceName = deviceid;
                user.Position = Location;
                user.eventName = eventName;
                user.eventId = eventId;
                user.sessionid=sessionid;
                user.ipaddress=ipaddress;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                user.BaseModel.OperationType = "RIFDRunningDelete";
                user.CreatedDate = DateTime.Now;

                //RFIDChestNoMappingDto user = new RFIDChestNoMappingDto
                //{
                //    UserId = userid,
                //    RecruitId = recruitid,
                //    DeviceName = deviceid,
                //    Position = Location,
                //    eventId = eventName,

                //    BaseModel = new BaseModel { OperationType = "RFIDRunningLog" },
                //    CreatedDate = DateTime.Now
                //};
                var result = await _candidateService.RFIDChestNoMapping(user);
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPost("RFIDRunningLog400meter")]
        public async Task<IActionResult> RFIDRunningLog400meter(
[FromQuery] string userid,
[FromQuery] string recruitid,
[FromQuery] string deviceid,
[FromQuery] string Location,
[FromQuery] string eventName,
[FromQuery] string eventId,
[FromBody] List<RFIDRunningLogItem> rfidData,
[FromQuery] string sessionid,
[FromQuery] string ipaddress)
        {
            try
            {
                if (rfidData == null || rfidData.Count == 0)
                    return BadRequest("No RFID data provided.");

                // Only Lap History Table (NEW)
                DataTable lapHistory = new DataTable();
                lapHistory.Columns.Add("RFID", typeof(string));
                lapHistory.Columns.Add("eventId", typeof(string));
                lapHistory.Columns.Add("CreatedBy", typeof(string));
                lapHistory.Columns.Add("CreatedDate", typeof(DateTime));
                lapHistory.Columns.Add("isactive", typeof(string));
                lapHistory.Columns.Add("currentDateTime", typeof(string));
                lapHistory.Columns.Add("Position", typeof(string));
                lapHistory.Columns.Add("Status", typeof(string));
                lapHistory.Columns.Add("DeviceName", typeof(string));
                lapHistory.Columns.Add("RecruitId", typeof(string));
                lapHistory.Columns.Add("LapCount", typeof(string));

                foreach (var item in rfidData)
                {
                    // Build laps manually
                    List<string> laps = new List<string>();

                    if (!string.IsNullOrEmpty(item.Lap1)) laps.Add(item.Lap1);
                    if (!string.IsNullOrEmpty(item.Lap2)) laps.Add(item.Lap2);
                    if (!string.IsNullOrEmpty(item.Lap3)) laps.Add(item.Lap3);
                    if (!string.IsNullOrEmpty(item.Lap4)) laps.Add(item.Lap4);
                    if (!string.IsNullOrEmpty(item.Lap5)) laps.Add(item.Lap5);

                    int lapNo = 1;

                    foreach (var lap in laps)
                    {
                        lapHistory.Rows.Add(
                            item.RFIDdtagata,
                            eventId,
                            userid,
                            DateTime.Now,
                            "1",  
                            lap,
                            Location,
                            "0",
                            deviceid,
                            recruitid,
                            lapNo.ToString()
                        );

                        lapNo++;
                    }
                }



                // DEBUG: check row count before sending
                Console.WriteLine("LapHistory Rows: " + lapHistory.Rows.Count);


                // Send ONLY lapHistory
                RFIDChestNoMappingDto user = new RFIDChestNoMappingDto
                {
                    UserId = userid,
                    RecruitId = recruitid,
                    DeviceName = deviceid,
                    Position = Location,
                    eventName = eventName,
                    eventId = eventId,
                    CreatedDate = DateTime.Now,
                    DataTable1 = lapHistory,     // ONLY THIS NOW
                    BaseModel = new BaseModel { OperationType = "RFIDRunningLog400meter" },
                    sessionid = sessionid,
                    ipaddress = ipaddress
                };

                var result = await _candidateService.RFIDRunningLog(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("RFIDRunningLog800Meter")]
        public async Task<IActionResult> RFIDRunningLog800Meter(
[FromQuery] string userid,
[FromQuery] string recruitid,
[FromQuery] string deviceid,
[FromQuery] string Location,
[FromQuery] string eventName,
[FromQuery] string eventId,
[FromBody] List<RFIDRunningLogItem> rfidData,
[FromQuery] string sessionid,
[FromQuery] string ipaddress)
        {
            try
            {
                if (rfidData == null || rfidData.Count == 0)
                    return BadRequest("No RFID data provided.");

                // Only Lap History Table (NEW)
                DataTable lapHistory = new DataTable();
                lapHistory.Columns.Add("RFID", typeof(string));
                lapHistory.Columns.Add("eventId", typeof(string));
                lapHistory.Columns.Add("CreatedBy", typeof(string));
                lapHistory.Columns.Add("CreatedDate", typeof(DateTime));
                lapHistory.Columns.Add("isactive", typeof(string));
                lapHistory.Columns.Add("currentDateTime", typeof(string));
                lapHistory.Columns.Add("Position", typeof(string));
                lapHistory.Columns.Add("Status", typeof(string));
                lapHistory.Columns.Add("DeviceName", typeof(string));
                lapHistory.Columns.Add("RecruitId", typeof(string));
                lapHistory.Columns.Add("LapCount", typeof(string));

                foreach (var item in rfidData)
                {
                    // Build laps manually
                    List<string> laps = new List<string>();

                    if (!string.IsNullOrEmpty(item.Lap1)) laps.Add(item.Lap1);
                    if (!string.IsNullOrEmpty(item.Lap2)) laps.Add(item.Lap2);
                    //if (!string.IsNullOrEmpty(item.Lap3)) laps.Add(item.Lap3);
                    //if (!string.IsNullOrEmpty(item.Lap4)) laps.Add(item.Lap4);
                    //if (!string.IsNullOrEmpty(item.Lap5)) laps.Add(item.Lap5);

                    int lapNo = 1;

                    foreach (var lap in laps)
                    {
                        lapHistory.Rows.Add(
                            item.RFIDdtagata,
                            eventId,
                            userid,
                            DateTime.Now,
                            "1",
                            lap,
                            Location,
                            "0",
                            deviceid,
                            recruitid,
                            lapNo.ToString()
                        );

                        lapNo++;
                    }
                }



                // DEBUG: check row count before sending
                Console.WriteLine("LapHistory Rows: " + lapHistory.Rows.Count);


                // Send ONLY lapHistory
                RFIDChestNoMappingDto user = new RFIDChestNoMappingDto
                {
                    UserId = userid,
                    RecruitId = recruitid,
                    DeviceName = deviceid,
                    Position = Location,
                    eventName = eventName,
                    eventId = eventId,
                    CreatedDate = DateTime.Now,
                    DataTable1 = lapHistory,     // ONLY THIS NOW
                    BaseModel = new BaseModel { OperationType = "RFIDRunningLog800Meter" },
                    sessionid = sessionid,
                    ipaddress = ipaddress
                };

                var result = await _candidateService.RFIDRunningLog(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        //[HttpPost("RFIDRunningLog")]
        //public async Task<IActionResult> RFIDRunningLog([FromQuery] string userid, [FromQuery] string recruitid, [FromQuery] string deviceid, [FromQuery] string Location, [FromQuery] string eventName, [FromBody] List<RFIDRunningLogItem> rfidData, [FromQuery] string sessionid, [FromQuery] string ipaddress)
        //{
        //    try
        //    {
        //        var results = new List<IActionResult>();
        //        foreach (var item in rfidData)
        //        {
        //            RFIDChestNoMappingDto user = new RFIDChestNoMappingDto
        //            {
        //                UserId = userid,
        //                RecruitId = recruitid,
        //                DeviceName = deviceid,
        //                Position = Location,
        //                eventId = eventName,
        //                RFID = item.RFIDdtagata,
        //                currentDateTime = item.Timestamp,
        //                BaseModel = new BaseModel { OperationType = "RFIDRunningLog" },
        //                CreatedDate = DateTime.Now,
        //                sessionid=sessionid,
        //                ipaddress=ipaddress
        //            };

        //            var result = await _candidateService.RFIDRunningLog(user);
        //            results.Add(result);
        //        }

        //        return Ok(results);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}
        [HttpPost("RFIDupload")]
        public async Task<IActionResult> UploadExcel(IFormFile file, [FromForm] string userId,[FromForm] string RecruitId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            RFIDChestNoMappingDto user = new RFIDChestNoMappingDto
            {
                BaseModel = new BaseModel { OperationType = "RFIDupload" },
                UserId = userId,
                RecruitId = RecruitId,
                CreatedDate = DateTime.Now
            };

            if (file == null || file.Length == 0)
            {
                return Ok(new Outcome { OutcomeId = 0, OutcomeDetail = "No data in the excel!" });
            }

            string[] allowedFileExtensions = { ".xls", ".xlsx", ".xlsm", ".csv" };
            if (!allowedFileExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                return BadRequest("Invalid file type");
            }

            // FORCE schema to match SQL TVP
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("RFID", typeof(string));
            dataTable.Columns.Add("ChestNo", typeof(string));
            dataTable.Columns.Add("Barcode", typeof(string));

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    if (worksheet.Dimension == null)
                    {
                        return Ok(new Outcome { OutcomeId = 0, OutcomeDetail = "Excel sheet empty" });
                    }

                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var dr = dataTable.NewRow();

                        dr["RFID"]    = worksheet.Cells[row, 1]?.Text?.Trim();
                        dr["ChestNo"] = worksheet.Cells[row, 2]?.Text?.Trim();
                        dr["Barcode"] = worksheet.Cells[row, 3]?.Text?.Trim();

                        dataTable.Rows.Add(dr);
                    }
                }
            }

            user.DataTable = dataTable;


            // 🔍 Debug safety
            if (user.DataTable.Rows.Count == 0)
            {
                return Ok(new Outcome { OutcomeId = 0, OutcomeDetail = "No valid rows found in Excel" });
            }

            return await _candidateService.RFIDChestNoMapping(user);
        }

        //[HttpPost("DeleteAllMapping")]
        //public async Task<IActionResult> DeleteAllMapping([FromBody] RFIDChestNoMappingDto user)
        //{
        //    try
        //    {
        //        if (user.BaseModel == null)
        //        {
        //            user.BaseModel = new BaseModel();
        //        }
        //        user.BaseModel.OperationType = "DeleteAllMapping";
        //        user.CreatedDate = DateTime.Now;
        //        var result = await _candidateService.Get(user);
        //        return result;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}
        public static class FileConverter
        {
            public static void ConvertCsvToXlsx(Stream inputStream, Stream outputStream)
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    using (var reader = new StreamReader(inputStream, Encoding.UTF8))
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

    }
}
