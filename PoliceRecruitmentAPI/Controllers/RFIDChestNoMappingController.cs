using common;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Data;
using System.Globalization;
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
                user.BaseModel.OperationType = "Insert";
                user.CreatedDate= DateTime.Now;
                var result = await _candidateService.Get(user);
                return result;
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
        [HttpPost("RIFDRunningDelete")]
        public async Task<IActionResult> RIFDRunningDelete([FromQuery] string userid, [FromQuery] string recruitid, [FromQuery] string deviceid, [FromQuery] string Location, [FromQuery] string eventName, [FromQuery] string sessionid, [FromQuery] string ipaddress)
        {
            RFIDChestNoMappingDto user = new RFIDChestNoMappingDto();
            try
            {
            
                user.UserId = userid;
                user.RecruitId = recruitid;
                user.DeviceName = deviceid;
                user.Position = Location;
                user.eventId = eventName;
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

        [HttpPost("RFIDRunningLog")]
        public async Task<IActionResult> RFIDRunningLog([FromQuery] string userid, [FromQuery] string recruitid, [FromQuery] string deviceid, [FromQuery] string Location, [FromQuery] string eventName, [FromBody] List<RFIDRunningLogItem> rfidData, [FromQuery] string sessionid, [FromQuery] string ipaddress)
        {
            RFIDChestNoMappingDto user = null;
            try
            {             
                var results = new List<IActionResult>();
                foreach (var item in rfidData)
                {
                    user = new RFIDChestNoMappingDto
                    {
                        UserId = userid,
                        RecruitId = recruitid,
                        DeviceName = deviceid,
                        Position = Location,
                        eventId = eventName,
                        RFID = item.RFIDdtagata,
                        currentDateTime = item.Timestamp,
                        BaseModel = new BaseModel { OperationType = "RFIDRunningLog" },
                        CreatedDate = DateTime.Now,
                        sessionid=sessionid,
                        ipaddress=ipaddress
                    };

                    var result = await _candidateService.RFIDRunningLog(user);
                    results.Add(result);
                }

                return Ok(results);
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
        [HttpPost("RFIDupload")]
        public async Task<IActionResult> UploadExcel(IFormFile file, [FromForm] string userId, [FromForm] string RecruitId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            RFIDChestNoMappingDto user = new RFIDChestNoMappingDto { BaseModel = new BaseModel { OperationType = "RFIDupload" } };
            user.UserId = userId;
            user.RecruitId = RecruitId;
            user.CreatedDate= DateTime.Now;
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
                            //var cellValue = worksheet.Cells[row, col].Value?.ToString();
                            var cellValue = worksheet.Cells[row, col]?.Value.ToString();
                            if (DateTime.TryParse(cellValue, out DateTime parsedDate))
                            {
                                dataRow[col - 1] = parsedDate.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                dataRow[col - 1] = cellValue?.ToString();
                            }
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }

            user.DataTable = dataTable;
            var parameter = await _candidateService.RFIDChestNoMapping(user);
            return parameter;
        }

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
