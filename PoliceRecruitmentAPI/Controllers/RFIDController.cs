using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Services.Interfaces;

namespace PoliceRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RFIDController : ControllerBase
    {
        private readonly IRfidRepository _rfidRepository;
        private static string _controllerCachedTag = null;

        public RFIDController(IRfidRepository rfidRepository)
        {
            _rfidRepository = rfidRepository;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartListener()
        {
            await _rfidRepository.StartListenerAsync();
            //return Ok("RFID listener started.");

            return NoContent();
        }


        [HttpPost("stop")]
        public IActionResult StopListener()
        {
            _rfidRepository.StopListener();
            // return Ok("RFID listener stopped.");
            return NoContent();
        }

        [HttpGet("ReadRFIDtag")]
        public async Task<IActionResult> GetLatestTag()
        {

            var result = await _rfidRepository.GetLatestTagAsync();
            return Ok(result);
        }
    }
}


