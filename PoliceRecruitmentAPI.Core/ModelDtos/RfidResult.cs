using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class RfidOutcome
    {
        public BaseModel? BaseModel { get; set; }
        public string? UserId { get; set; }
        public string? Accesstoken { get; set; }
        public string? refershtoken { get; set; }
        public RfidResult? Result { get; set; }
        // New property to store tag ID at the top level
        public string? TagId { get; set; }
        public string? RecruitId { get; set; }
    }

    public class RfidResult
    {
        public string? Tag { get; set; }
        public string? Message { get; set; }
        public string? ConnectionStatus { get; set; }
        public bool IsConnected { get; set; }
    }
}
