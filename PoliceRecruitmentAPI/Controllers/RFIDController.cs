using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;
using PoliceRecruitmentAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;
using PoliceRecruitmentAPI.Core.Repository;
using Context;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using common;
using Newtonsoft.Json;

namespace PoliceRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExampleFilterAttribute]
    public class RFIDController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IRfidRepository _rfidRepository;
        //private readonly ILogger<RFIDController> _logger;


        public RFIDController(IRfidRepository rfidRepository, IConfiguration configuration, ILogger<RFIDController> logger)
        {
            _rfidRepository = rfidRepository;
            _configuration=configuration;
            //_logger = logger;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartListener()
        {
            await _rfidRepository.StartListenerAsync();
            return NoContent();
        }

        [HttpPost("stop")]
        public async Task<IActionResult> StopListener()
        {
            await _rfidRepository.StopListener();
            return NoContent();
        }

        [HttpGet("ReadRFIDtag")]
        public async Task<IActionResult> GetLatestTag([FromQuery] RfidOutcome rfidTagDto)
        {
            try
            {
                if (rfidTagDto.BaseModel == null)
                {
                    rfidTagDto.BaseModel = new BaseModel();
                }

                rfidTagDto.BaseModel.OperationType = "GetLatestTag";

                // Get the tag from the repository
                var Result = await _rfidRepository.GetLatestTagAsync(rfidTagDto);
                if (Result is ObjectResult objectResult)
                {
                    var actualData = objectResult.Value; // Extracting the actual data
                    if (actualData != null)
                    {
                        Console.WriteLine($"Extracted Data: {JsonConvert.SerializeObject(actualData)}");
                        return Ok(actualData);
                        //Returning only the actual data
                    }
                }
                if (string.IsNullOrEmpty(rfidTagDto.Accesstoken) && Request.Headers.ContainsKey("Authorization"))
                {
                    string authHeader = Request.Headers["Authorization"];
                    if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                    {
                        rfidTagDto.Accesstoken = authHeader.Replace("Bearer ", "");
                    }
                }
                //var jwtService = new jwtTokenCreate(_configuration);
                //var jwtToken = jwtService.createToken(rfidTagDto.UserId);

                //// Convert JWT to string format
                //rfidTagDto.refershtoken = new JwtSecurityTokenHandler().WriteToken(jwtToken);


                // Create a response in the format shown in the example
                //var response = new
                //{
                //    Outcome = new
                //    {
                //        OutcomeId = 1,
                //        OutcomeDetail = "GetLatestTag",
                //        //tokens = rfidTagDto.Accesstoken,
                //        //userId = rfidTagDto.UserId,
                //        //recruitId = rfidTagDto.RecruitId,  
                //        //refreshToken = rfidTagDto.refershtoken,

                //    },
                //    data = new[]
                //    {
                //        new
                //        {
                //            connectionStatus = result.Result.ConnectionStatus,
                //            isConnected = result.Result.IsConnected,
                //            message = result.Result.Message,
                //            tagId = result.TagId,
                //             BaseModel1 = rfidTagDto.BaseModel,
                //        }
                //    },


                //};
                return Ok(Result);
                // return Ok(actualData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


    }
}