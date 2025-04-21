using common;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.ApiServices;
using PoliceRecruitmentAPI.Services.Interfaces;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace PoliceRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExampleFilterAttribute]
    public class CandidateController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ILogger<CandidateController> _logger;
        public readonly ICandidateService _candidateService;
        public readonly IRecruitmentEventService _recruitmentEventService;

        public CandidateController(ILogger<CandidateController> logger, IConfiguration configuration, ICandidateService candidateService, IRecruitmentEventService recruitmentEventService)
        {
            _logger = logger;
            _configuration = configuration;
            _candidateService = candidateService;
            _recruitmentEventService = recruitmentEventService;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CandidateDto user)
        {
            try

            {
                //CandidateDto user=new CandidateDto();
                //user.CandidateID= CandidateID;
                //user.Cast= Cast;
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }
                if (user.CandidateID == null)
                {
                    user.BaseModel.OperationType = "Insert";
                }
                else
                {
                    user.BaseModel.OperationType = "Update";
                }
                var createduser = await _candidateService.Candidate(user);
                return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetCandidate")]
        public async Task<IActionResult> GetCandidate([FromQuery] CandidateDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "Get";

                dynamic userDetail = await _candidateService.Get(model);

                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] CandidateDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAll";

                dynamic userDetail = await _candidateService.Candidate(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }


        [HttpGet("GetCandidateGender")]
        public async Task<IActionResult> GetCandidateGender([FromQuery] CandidateDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetCandidateGender";

                dynamic userDetail = await _candidateService.Candidate(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpGet("GetGroup")]
        public async Task<IActionResult> GetGroup([FromQuery] CandidateDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetGroup";

                dynamic userDetail = await _candidateService.Candidate(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpGet("GetAllChestNo")]
        public async Task<IActionResult> GetAllChestNo([FromQuery] CandidateDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAllChestNo";

                dynamic userDetail = await _candidateService.Candidate(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }


        [HttpGet("Candidatefilterdata")]
        public async Task<IActionResult> Canddattefilterdata([FromQuery] CandidateDto model)
        {
            try
            {
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetCandidatesCompleted";

                dynamic userDetail = await _candidateService.Candidate(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }


        [HttpGet("GetAllValue")]
        public async Task<IActionResult> GetAllValue([FromQuery] CandidateDto model)
        {

            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAllValue";

                dynamic userDetail = await _candidateService.Candidate1(model);

                string RecruitId = model.RecruitId;

                string UserId = model.UserId;
                var recruitmentEventDto = new RecruitmentEventDto
                {
                    recConfId = RecruitId,
                    UserId = UserId,
                    BaseModel = new BaseModel { OperationType = "GetAllRecruitEvent1" }
                };
                dynamic eventDetail = await _recruitmentEventService.RecruitEvent(recruitmentEventDto);
                dynamic tempoutdetail = userDetail.Value.Outcome;
                var categoryData = new Dictionary<string, List<dynamic>>();

                foreach (var item in userDetail.Value.Data)
                {
                    string category = item.Category;

                    if (!categoryData.ContainsKey(category))
                    {
                        categoryData[category] = new List<dynamic>();
                    }
                    categoryData[category].Add(item);
                }

                dynamic resultData = categoryData.Select(kvp => new
                {
                    Category = kvp.Key,
                    Items = kvp.Value
                }).ToList();

                dynamic finaldata = new { data = resultData, Eventlist = eventDetail };
                dynamic Result = new
                {
                    Data = finaldata,
                    UserId = UserId,
                    Outcome = tempoutdetail
                };
                if (Result == null)
                {
                    return NotFound(new { message = "No data found." });
                }

                return Ok(Result);

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpGet("GetAllValueGround")]


        public async Task<IActionResult> GetAllValueGround([FromQuery] CandidateDto model)
        {
            try
            {
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAllValueGround";

                dynamic userDetail = await _candidateService.Candidate1(model);
                string RecruitId = model.RecruitId;
                string UserId = model.UserId;

                var recruitmentEventDto = new RecruitmentEventDto
                {
                    recConfId = RecruitId,
                    UserId = UserId,
                    BaseModel = new BaseModel { OperationType = "GetAllRecruitEvent1" }
                };

                dynamic eventDetail = await _recruitmentEventService.RecruitEvent(recruitmentEventDto);
                dynamic tempoutdetail = userDetail.Value.Outcome;

                var categoryData = new Dictionary<string, List<dynamic>>();

                foreach (var item in userDetail.Value.Data)
                {
                    string category = item.Category;

                    if (!categoryData.ContainsKey(category))
                    {
                        categoryData[category] = new List<dynamic>();
                    }
                    categoryData[category].Add(item);
                }

                dynamic resultData = categoryData.Select(kvp => new
                {
                    Category = kvp.Key,
                    Items = kvp.Value
                }).ToList();
                dynamic finaldata = new { data = resultData, Eventlist = eventDetail };
                dynamic Result = new
                {
                    Data = finaldata,
                    UserId = UserId,
                    Outcome = tempoutdetail
                };

                if (Result == null)
                {
                    return NotFound(new { message = "No data found." });
                }

                return Ok(Result);
            }
            catch (Exception ex)
            {
                // Return a consistent ObjectResult for errors
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }


        [HttpGet("GetMeritnCastwiseCandidate")]
        public async Task<IActionResult> GetMeritnCastwiseCandidate([FromQuery] CandidateDto model)
        {
            try
            {
                // Ensure the BaseModel exists and set the OperationType to "GetCastwiseCandidate"
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }

                // Set OperationType in the backend
                model.BaseModel.OperationType = "GetMeritnCastwiseCandidate";

                // Call the service to fetch the data based on the OperationType
                dynamic userDetail = await _candidateService.Candidate2(model);

                // Combine the data with additional necessary information (e.g., event details)
                string RecruitId = model.RecruitId;
                string UserId = model.UserId;

                var recruitmentEventDto = new RecruitmentEventDto
                {
                    recConfId = RecruitId,
                    UserId = UserId,
                    BaseModel = new BaseModel { OperationType = "GetAllRecruitEvent1" }
                };

                // Fetch event details
                dynamic eventDetail = await _recruitmentEventService.RecruitEvent(recruitmentEventDto);

                // Combine user details (from Candidate service) and event details
                dynamic tempoutdetail = userDetail.Value.Outcome;
                dynamic finalData = new
                {
                    data = userDetail,
                    Eventlist = eventDetail
                };

                // Create the final result object
                dynamic result = new
                {
                    Data = finalData,
                    UserId = UserId,
                    Outcome = tempoutdetail
                };

                // Return the result
                if (result == null)
                {
                    return NotFound(new { message = "No data found." });
                }

                return Ok(result);  // Return the response with the data (MeritList, CastWisedata, and Outcome)
            }
            catch (Exception ex)
            {
                // Handle exceptions and return a meaningful error message
                return new JsonResult(new { message = ex.Message })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        //public async Task<IActionResult> GetAllValueGround([FromQuery] CandidateDto model)
        //{

        //    try
        //    {

        //        if (model.BaseModel == null)
        //        {
        //            model.BaseModel = new BaseModel();
        //        }
        //        model.BaseModel.OperationType = "GetAllValueGround";

        //        dynamic userDetail = await _candidateService.Candidate1(model);

        //        string RecruitId = model.RecruitId;

        //        string UserId = model.UserId;
        //        var recruitmentEventDto = new RecruitmentEventDto
        //        {
        //            recConfId = RecruitId,
        //            UserId = UserId,
        //            BaseModel = new BaseModel { OperationType = "GetAllRecruitEvent1" }
        //        };
        //        dynamic eventDetail = await _recruitmentEventService.RecruitEvent(recruitmentEventDto);
        //        dynamic tempoutdetail = userDetail.Value.Outcome;

        //        var categoryData = new Dictionary<string, List<dynamic>>();


        //        foreach (var item in userDetail.Value.Data)
        //        {
        //            string category = item.Category; 

        //            if (!categoryData.ContainsKey(category))
        //            {
        //                categoryData[category] = new List<dynamic>();
        //            }
        //            categoryData[category].Add(item);
        //        }


        //        var resultData = categoryData.Select(kvp => new
        //        {
        //            Category = kvp.Key,
        //            Items = kvp.Value
        //        }).ToList();
        //         dynamic finaldata = new { data = userDetail, Eventlist = eventDetail };
        //        dynamic Result = new
        //        {
        //            Data = resultData,
        //            UserId = UserId,
        //            Outcome = tempoutdetail
        //        };

        //        if (Result == null)
        //        {
        //            return NotFound(new { message = "No data found." });
        //        }
        //        //dynamic finaldata = new { data = userDetail, Eventlist = eventDetail };

        //        //dynamic Result = new { Data = finaldata, UserId = UserId, Outcome = tempoutdetail };
        //        //if (Result == null)
        //        //{
        //        //    return NotFound(new { message = "No data found." });
        //        //}
        //        return Ok(Result);

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        //    }
        //}

        [HttpGet("GetCastwiseCandidate")]
        public async Task<IActionResult> GetCastwiseCandidate([FromQuery] CandidateDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetCastwiseCandidate";

                dynamic userDetail = await _candidateService.Candidate(model);
                string RecruitId = model.RecruitId;

                string UserId = model.UserId;
                var recruitmentEventDto = new RecruitmentEventDto
                {
                    recConfId = RecruitId,
                    UserId = UserId,
                    BaseModel = new BaseModel { OperationType = "GetAllRecruitEvent1" }
                };
                dynamic eventDetail = await _recruitmentEventService.RecruitEvent(recruitmentEventDto);
                dynamic tempoutdetail = userDetail.Value.Outcome;
                dynamic finaldata = new { data = userDetail, Eventlist = eventDetail };
                dynamic Result = new { Data = finaldata, UserId = UserId, Outcome = tempoutdetail };
                if (Result == null)
                {
                    return NotFound(new { message = "No data found." });
                }
                return Ok(Result);

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        //[HttpGet("GetCastwiseCandidate")]
        //public async Task<IActionResult> GetCastwiseCandidate()
        //{
        //    try
        //    {
        //        CandidateDto model = new CandidateDto();
        //        if (model.BaseModel == null)
        //        {
        //            model.BaseModel = new BaseModel();
        //        }
        //        model.BaseModel.OperationType = "GetCastwiseCandidate";

        //        dynamic userDetail = await _candidateService.Candidate1(model);
        //        return userDetail;

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        //    }
        //}

        [HttpGet("GetPassCandidate")]
        public async Task<IActionResult> GetPassCandidate()
        {
            try
            {
                CandidateDto model = new CandidateDto();
                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetPassCandidate";

                dynamic userDetail = await _candidateService.Candidate(model);
                return userDetail;

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcel(IFormFile file, [FromForm] string userId, [FromForm] string RecruitId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            CandidateDto user = new CandidateDto { BaseModel = new BaseModel { OperationType = "InsertData" } };
            user.UserId = userId;
            user.RecruitId = RecruitId;
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
                            var cellValue = worksheet.Cells[row, col].Value?.ToString();
                            if (DateTime.TryParse(cellValue, out DateTime parsedDate))
                            {
                                dataRow[col - 1] = parsedDate.ToString("yyyy-MM-dd");
                            }
                            else
                            {
                                dataRow[col - 1] = cellValue;
                            }
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }

            user.DataTable = dataTable;
            var parameter = await _candidateService.Candidate(user);
            return parameter;
        }



        [HttpPost("uploadCandidateNew")]
        public async Task<IActionResult> uploadCandidateNew(IFormFile file, [FromForm] string userId, [FromForm] string RecruitId)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            CandidateDto user = new CandidateDto();
            if (user.BaseModel == null)
            {
                user.BaseModel = new BaseModel();
            }

            user.UserId = userId;
            user.RecruitId = RecruitId;

            if (file == null || file.Length == 0)
            {
                return StatusCode(422, new Outcome { OutcomeId = 3, OutcomeDetail = "No data in the excel!" });
            }

            string[] allowedFileExtensions = { ".xls", ".xlsx", ".xlsm", ".csv" };
            if (!allowedFileExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                ModelState.AddModelError("File", "Please upload a file of type: " + string.Join(", ", allowedFileExtensions));
                return BadRequest(ModelState);
            }

            DataTable dataTable2 = new DataTable();
            List<string> errorLog = new List<string>();

            HashSet<string> mobilenosSeen = new HashSet<string>();
            HashSet<string> applicationNosSeen = new HashSet<string>();
            
            // Fetch existing MobileNos and ApplicationNos (as done in your code)
            user.BaseModel.OperationType = "ExistingCandidateMobileNo";
            dynamic existingMobileNo = await _candidateService.Candidate(user);

            HashSet<string> existingMobileNos = new HashSet<string>();
            foreach (var mobilenoObj in existingMobileNo.Value.Data)
            {
                string mobileNo = mobilenoObj.MobileNumber?.ToString().Trim();
                if (!string.IsNullOrEmpty(mobileNo))
                {
                    existingMobileNos.Add(mobileNo);
                }
            }

            user.BaseModel.OperationType = "ExistingCandidateApplicationNo";
            dynamic existingApplicationNo1 = await _candidateService.Candidate(user);

            HashSet<string> existingApplicationNo = new HashSet<string>();
            foreach (var applicationObj in existingApplicationNo1.Value.Data)
            {
                string applicationNo = applicationObj.ApplicationNo?.ToString().Trim();
                if (!string.IsNullOrEmpty(applicationNo))
                {
                    existingApplicationNo.Add(applicationNo);
                }
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                if (Path.GetExtension(file.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    using (var reader = new StreamReader(stream))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Quote = '"',
                        Delimiter = ",",
                        BadDataFound = null // To ignore invalid data rows
                    }))
                    {
                        var records = csv.GetRecords<dynamic>().ToList();

                        if (records.Any())
                        {
                            var header = ((IDictionary<string, object>)records[0]).Keys.ToList();
                            foreach (var column in header)
                            {
                                dataTable2.Columns.Add(new DataColumn(column, typeof(string)));
                            }

                            foreach (var record in records)
                            {
                                var dataRow = dataTable2.NewRow();
                                foreach (var column in header)
                                {
                                    dataRow[column] = ((IDictionary<string, object>)record)[column]?.ToString();
                                }

                                string genders = dataRow["Gender"]?.ToString()?.Trim();
                                if (string.IsNullOrEmpty(genders))
                                {
                                    errorLog.Add($"Gender :{genders}");
                                }
                                else
                                {
                                    applicationNosSeen.Add(genders);
                                }

                                string parallelreservation = dataRow["Parallel Reservation"]?.ToString()?.Trim();
                                if (string.IsNullOrEmpty(parallelreservation))
                                {
                                    errorLog.Add($"ParallelReservation :{parallelreservation}");
                                }
                                else
                                {
                                    applicationNosSeen.Add(parallelreservation);
                                }

                                string mobileno = dataRow["MobileNumber"]?.ToString()?.Trim();
                                if (string.IsNullOrEmpty(mobileno))
                                {
                                    errorLog.Add($"MobileNumber :{mobileno}");
                                }
                                else
                                if (mobilenosSeen.Contains(mobileno) || existingMobileNos.Contains(mobileno))
                                {
                                    errorLog.Add($"MobileNo: {mobileno}");
                                }
                                else
                                {
                                    mobilenosSeen.Add(mobileno);
                                }

                                string applicationNo = dataRow["ApplicationNo"]?.ToString()?.Trim();
                                if (string.IsNullOrEmpty(applicationNo))
                                {
                                    errorLog.Add($"ApplicationNumber:{applicationNo}");
                                }
                                else
                               if (applicationNosSeen.Contains(applicationNo) || existingApplicationNo.Contains(applicationNo))
                                {
                                    errorLog.Add($"ApplicationNo: {applicationNo}");
                                }
                                else
                                {
                                    applicationNosSeen.Add(applicationNo);
                                }

                                dataTable2.Rows.Add(dataRow);
                            }
                        }
                        else
                        {
                            return StatusCode(422, new Outcome { OutcomeId = 3, OutcomeDetail = "No data in the excel!" });
                        }
                    }
                }
                else
                {
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
                        }

                        for (int col = 1; col <= colCount; col++)
                        {
                            string columnName = worksheet.Cells[1, col].Value?.ToString();
                            if (!string.IsNullOrEmpty(columnName))
                            {
                                dataTable2.Columns.Add(new DataColumn(columnName, typeof(string)));
                            }
                        }

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var dataRow = dataTable2.NewRow();
                            for (int col = 1; col <= colCount; col++)
                            {
                                var cellValue = worksheet.Cells[row, col].Value?.ToString();
                                if (DateTime.TryParse(cellValue, out DateTime parsedDate))
                                {
                                    dataRow[col - 1] = parsedDate.ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    dataRow[col - 1] = cellValue;
                                }
                            }

                            string genders = dataRow["Gender"]?.ToString()?.Trim();
                            if (string.IsNullOrEmpty(genders))
                            {
                                errorLog.Add($"Gender :{genders}");
                            }
                            else
                            {
                                applicationNosSeen.Add(genders);
                            }
                            string parallelreservation = dataRow["Parallel Reservation"]?.ToString()?.Trim();
                            if (string.IsNullOrEmpty(parallelreservation))
                            {
                                errorLog.Add($"ParallelReservation :{parallelreservation}");
                            }
                            else
                            {
                                applicationNosSeen.Add(parallelreservation);
                            }
                            string mobileno = dataRow["MobileNumber"]?.ToString()?.Trim();
                            if (string.IsNullOrEmpty(mobileno))
                            {
                                errorLog.Add($"MobileNumber :{mobileno}");
                            }
                            else
                            if (mobilenosSeen.Contains(mobileno) || existingMobileNos.Contains(mobileno))
                            {
                                errorLog.Add($"MobileNo: {mobileno}");
                            }
                            else
                            {
                                mobilenosSeen.Add(mobileno);
                            }

                            string applicationNo = dataRow["ApplicationNo"]?.ToString()?.Trim();
                            if (string.IsNullOrEmpty(applicationNo))
                            {
                                errorLog.Add($"ApplicationNumber :{applicationNo}");
                            }
                            else
                            if (applicationNosSeen.Contains(applicationNo) || existingApplicationNo.Contains(applicationNo))
                            {
                                errorLog.Add($"ApplicationNo: {applicationNo}");
                            }
                            else
                            {
                                applicationNosSeen.Add(applicationNo);
                            }

                            dataTable2.Rows.Add(dataRow);
                        }
                    }
                }
            }

            // Create separate error logs for MobileNo and ApplicationNo
            var mobileNoErrors = errorLog.Where(e => e.Contains("MobileNo")).ToList();
            var applicationNoErrors = errorLog.Where(e => e.Contains("ApplicationNo")).ToList();
            var blankMobileNoErrors = errorLog.Where(e => e.Contains("MobileNumber")).ToList();
            var blankApplicationNoErrors = errorLog.Where(e => e.Contains("ApplicationNumber")).ToList();
            var blanGenderErrors = errorLog.Where(e => e.Contains("Gender")).ToList();
            var blankparallelreservationErrors = errorLog.Where(e => e.Contains("ParallelReservation")).ToList();
             if (errorLog.Any())
             {
                var response = new
                {
                    OutcomeId = 4,
                    MobileNoErrors = mobileNoErrors,
                    ApplicationNoErrors = applicationNoErrors,
                    BlankMobileNoErrors = blankMobileNoErrors,
                    BlankApplicationNoErrors = blankApplicationNoErrors,
                    BlankGenderErrors = blanGenderErrors,
                    BlankParallelReservationErrors = blankparallelreservationErrors
                };
                return StatusCode(409, response);
            }

            user.BaseModel.OperationType = "InsertDataNewCandidate";
            user.DataTable2 = dataTable2;
            var parameter = await _candidateService.Candidate(user);
            return parameter;
        }

       
        //public async Task<IActionResult> uploadCandidateNew(IFormFile file, [FromForm] string userId, [FromForm] string RecruitId)
        //{
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //    CandidateDto user = new CandidateDto { BaseModel = new BaseModel { OperationType = "InsertDataNewCandidate" } };
        //    user.UserId = userId;
        //    user.RecruitId = RecruitId;
        //    if (file == null || file.Length == 0)
        //    {
        //        return Ok(new Outcome { OutcomeId = 0, OutcomeDetail = "No data in the excel!" });
        //    }

        //    string[] allowedFileExtensions = { ".xls", ".xlsx", ".xlsm", ".csv" };
        //    if (!allowedFileExtensions.Contains(Path.GetExtension(file.FileName)))
        //    {
        //        ModelState.AddModelError("File", "Please upload a file of type: " + string.Join(", ", allowedFileExtensions));
        //        return BadRequest(ModelState);
        //    }

        //    DataTable dataTable2 = new DataTable();



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
        //                return Ok(new Outcome { OutcomeId = 0, OutcomeDetail = "No data in the excel!" });
        //            }

        //            // Adding columns to DataTable based on Excel header row (first row)
        //            for (int col = 1; col <= colCount; col++)
        //            {
        //                string columnName = worksheet.Cells[1, col].Value?.ToString();
        //                if (!string.IsNullOrEmpty(columnName))
        //                {
        //                    dataTable2.Columns.Add(new DataColumn(columnName, typeof(string)));
        //                }
        //            }

        //            // Adding rows to DataTable from Excel data
        //            for (int row = 2; row <= rowCount; row++)
        //            {
        //                var dataRow = dataTable2.NewRow();
        //                for (int col = 1; col <= colCount; col++)
        //                {
        //                    var cellValue = worksheet.Cells[row, col].Value?.ToString();
        //                    if (DateTime.TryParse(cellValue, out DateTime parsedDate))
        //                    {
        //                        dataRow[col - 1] = parsedDate.ToString("yyyy-MM-dd");
        //                    }
        //                    else
        //                    {
        //                        dataRow[col - 1] = cellValue;
        //                    }
        //                }
        //                dataTable2.Rows.Add(dataRow);
        //            }
        //        }
        //    }

        //    user.DataTable2 = dataTable2;
        //    var parameter = await _candidateService.Candidate(user);
        //    return parameter;
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
        [HttpGet("download")]
        public IActionResult DownloadExcel()
        {
            // Define the column names for the Excel file
            string[] columnNames = {
        "RecruitmentYear", "OfficeName", "PostName", "ApplicationNo", "ExaminationFee",
        "FullNameDevnagari", "FullNameEnglish", "MothersName", "Gender",
        "MaritalStatus", "PassCertificationNo", "DOB", "Age", "Address", "PinCode",
        "MobileNumber", "EmailId", "PermanentAddress", "PermanentPinCode", "Nationality",
        "Religion", "Cast", "SubCast", "PartTime", "ProjectSick", "ExServiceman",
        "EarthquakeAffected","Orphan","Ancestral"
    };

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



        [HttpGet("GetAllDocuments")]
        public async Task<IActionResult> GetAllDocuments([FromQuery] CandidateDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetAllDocuments";

                dynamic userDetail = await _candidateService.Candidate(model);
                string UserId = model.UserId;
                model.BaseModel.OperationType = "GetAllDocumentsCastDocumnetStatus";

                dynamic usercaststatus = await _candidateService.Candidate(model);
                dynamic tempoutdetail = userDetail.Value.Outcome;
                dynamic finaldata = new { Data = userDetail, StausData = usercaststatus  };
                dynamic Result = new { Data = finaldata, UserId = UserId, Outcome = tempoutdetail };
                if (Result == null)
                {
                    return NotFound(new { message = "No data found." });
                }
                return Ok(Result);

            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }


        [HttpGet("GetmandatoryDocuments")]
        public async Task<IActionResult> GetmandatoryDocuments([FromQuery] CandidateDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "GetmandatoryDocuments";

                dynamic userDetail = await _candidateService.Candidate(model);
                return userDetail;


            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }

        [HttpPost("InsertGroundtestdata")]
        public async Task<IActionResult> InsertGroundtestdata([FromBody] CandidateDto user)
        {
            try
            {
                if (user.BaseModel == null)
                {
                    user.BaseModel = new BaseModel();
                }

                if (user.Id == null)
                {
                    user.BaseModel.OperationType = "InsertGrountTestResult";
                }
                else
                {
                    user.BaseModel.OperationType = "UpdateGrountTestResult";
                }
                //user.Createddate = DateTime.Now;
                //user.Updateddate = DateTime.Now;
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ResulType", typeof(string));
                dataTable.Columns.Add("ApplicationNo", typeof(string));
                dataTable.Columns.Add("CandidateID", typeof(string));
                dataTable.Columns.Add("FullNameDevnagari", typeof(string));
                dataTable.Columns.Add("FullNameEnglish", typeof(string));
                dataTable.Columns.Add("DOB", typeof(string));
                dataTable.Columns.Add("Cast", typeof(string));
                dataTable.Columns.Add("Category", typeof(string));
                dataTable.Columns.Add("gender", typeof(string));
                dataTable.Columns.Add("chestno", typeof(string));
                dataTable.Columns.Add("HundredMeterRunning", typeof(string));
                dataTable.Columns.Add("SixteenHundredMeterRunning", typeof(string));
                dataTable.Columns.Add("eightHundredMeterRunning", typeof(string));
                dataTable.Columns.Add("ShotPut", typeof(string));
                dataTable.Columns.Add("TotalScore", typeof(string));
                dataTable.Columns.Add("SequenceID", typeof(string));
                foreach (var categoryData in user.Groundtestdata1)
                {
                    foreach (var candidate in categoryData.Items)
                    {
                        dataTable.Rows.Add(
                             candidate.ResulType,
                            candidate.ApplicationNo,
                            candidate.CandidateID,
                            candidate.FullNameDevnagari,
                            candidate.FullNameEnglish,
                            candidate.DOB,
                            candidate.Cast,
                            candidate.Category,
                            candidate.gender,
                            candidate.chestno,
                            candidate.HundredMeterRunning,
                            candidate.SixteenHundredMeterRunning,
                            candidate.eightHundredMeterRunning,
                            candidate.ShotPut,
                            candidate.TotalScore,
                            candidate.SequenceID
                        );
                    }
                }
                //foreach (var privilage in user.Groundtestdata)
                //    {
                //        dataTable.Rows.Add(
                //            privilage.ApplicationNo,
                //            privilage.CandidateID,
                //            privilage.FullNameDevnagari,
                //            privilage.FullNameEnglish,
                //            privilage.DOB,
                //            privilage.Cast,
                //            privilage.Category,
                //            privilage.gender,
                //            privilage.chestno,
                //            privilage.HundredMeterRunning,
                //            privilage.SixteenHundredMeterRunning,
                //            privilage.eightHundredMeterRunning,
                //            privilage.ShotPut,
                //            privilage.TotalScore,
                //             privilage.SequenceID

                //        );
                //    }
                // user.Privilage = null;
                user.DataTable1 = dataTable;
                dynamic createduser = await _candidateService.Get(user);

                string RecruitId = user.RecruitId;

                string UserId = user.UserId;
                var recruitmentEventDto = new RecruitmentEventDto
                {
                    recConfId = RecruitId,
                    UserId = UserId,
                    BaseModel = new BaseModel { OperationType = "GetAllRecruitEvent1" }
                };
                dynamic eventDetail = await _recruitmentEventService.RecruitEvent(recruitmentEventDto);

                user.BaseModel.OperationType = "GetGrounTestResult";

                dynamic testDetail = await _candidateService.Candidate(user);
                dynamic tempoutdetail = testDetail.Value.Outcome;
                dynamic finaldata = new { data = createduser, Eventlist = eventDetail, Testdata = testDetail };
                dynamic Result = new { Data = finaldata, UserId = UserId, Outcome = tempoutdetail };
                if (Result == null)
                {
                    return NotFound(new { message = "No data found." });
                }
                return Ok(Result);
                // return createduser;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost("DeleteGrounTestResult")]
        public async Task<IActionResult> DeleteGrounTestResult([FromBody] CandidateDto model)
        {
            try
            {

                if (model.BaseModel == null)
                {
                    model.BaseModel = new BaseModel();
                }
                model.BaseModel.OperationType = "DeleteGrounTestResult";

                var result = await _candidateService.Candidate(model);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpGet("GetShotPutScore")]
        //public async Task<IActionResult> GetShotPutAll()
        //{
        //    try
        //    {
        //        CandidateDto model = new CandidateDto();
        //        if (model.BaseModel == null)
        //        {
        //            model.BaseModel = new BaseModel();
        //        }
        //        model.BaseModel.OperationType = "GetShotPut";

        //        dynamic userDetail = await _candidateService.Candidate(model);
        //        return userDetail;

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        //    }
        //}
        //[HttpGet("Get100mAll")]
        //public async Task<IActionResult> Get100mAll()
        //{
        //    try
        //    {
        //        CandidateDto model = new CandidateDto();
        //        if (model.BaseModel == null)
        //        {
        //            model.BaseModel = new BaseModel();
        //        }
        //        model.BaseModel.OperationType = "Get100mAll";

        //        dynamic userDetail = await _candidateService.Candidate(model);
        //        return userDetail;

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        //    }
        //}

        //[HttpGet("Get800/1600mAll")]
        //public async Task<IActionResult> Get800mAll()
        //{
        //    try
        //    {
        //        CandidateDto model = new CandidateDto();
        //        if (model.BaseModel == null)
        //        {
        //            model.BaseModel = new BaseModel();
        //        }
        //        model.BaseModel.OperationType = "Get800mAll";

        //        dynamic userDetail = await _candidateService.Candidate(model);
        //        return userDetail;

        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { message = ex.Message }) { StatusCode = StatusCodes.Status500InternalServerError };
        //    }
        //}
    }
}
