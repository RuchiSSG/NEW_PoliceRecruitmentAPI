using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class CandidateDailyReportDto
    {
        public BaseModel? BaseModel { get; set; }
         public string? UserId { get; set; }
        public string? RecruitId { get; set; }
        public string? All { get; set; }
        public string? documentdata { get; set; }
        public string? heichestdata { get; set; }
        public string? Eventid { get; set; }
        public DateTime? SelectedDate { get; set; }

    }
}
