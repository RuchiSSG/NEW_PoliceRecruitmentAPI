using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class RFIDRunningLogItem
    {
        public string? RFIDdtagata { get; set; }
        public string? Lap1 { get; set; }
        public string? Lap2 { get; set; }
        public string? Lap3 { get; set; }
        public string? Lap4 { get; set; }
        public string? Lap5 { get; set; }
        public int TotalLaps { get; set; }
        public List<string>? AllLaps { get; set; }
        public string? EventName { get; set; }
       // public string RFIDdtagata { get; set; }
        public string Timestamp { get; set; }
    }
}
