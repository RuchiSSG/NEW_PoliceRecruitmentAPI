using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoliceRecruitmentAPI.Core.ModelDtos;

namespace PoliceRecruitmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogController : ControllerBase
    {
        protected readonly ILogger _logger;

        protected ErrorLogController()
        {
        }

        protected IActionResult HandleException(Exception ex, string operationType)
        {
            var errorId = Guid.NewGuid().ToString("N");
            var errorResponse = new LogErrorResponse
            {
                ErrorId = errorId,
                Timestamp = DateTime.UtcNow,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                OperationType = operationType
            };

            _logger?.LogError(ex,
                "Error ID: {ErrorId}, Operation: {OperationType}, Message: {Message}",
                errorId,
                operationType,
                ex.Message);

            return new JsonResult(errorResponse)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}

