using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class LogErrorResponse
    {
        public const string? SEPARATOR_LINE = "=====================================================================================================================================";
        public string? ErrorId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public string? OperationType { get; set; }
        public string? FormattedTimestamp => Timestamp.ToString("dd-MM-yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
    }
}
