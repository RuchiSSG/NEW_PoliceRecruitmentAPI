using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class RfidResult
    {

        public string? Tag { get; set; }
        public string? ConnectionStatus { get; set; }
        public bool IsConnected { get; set; }
        public string? Message { get; set; }
    }
}
