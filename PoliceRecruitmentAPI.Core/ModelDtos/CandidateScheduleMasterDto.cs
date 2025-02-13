using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class CandidateScheduleMasterDto
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? Id { get; set; }
        public string? UserId { get; set; }
        public string? RecruitId { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public DateTime? NewScheduleDate { get; set; }
        public string?  ScheduleID { get; set; }
        public string? CandidateId { get; set; }

    }
}
