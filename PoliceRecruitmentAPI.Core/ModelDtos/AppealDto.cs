using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
	public class AppealDto
	{
		public BaseModel? BaseModel { get; set; }
		public long? CandidateID { get; set; }
		public string? UserId { get; set; }
		public string? ApprovedBy { get; set; }
		public DateTime? Date { get; set; }
		public string? Remark { get; set; }
        public string? Status { get; set; }
        public string? RecruitId { get; set; }
		public string? Cast { get; set; }
		public string ? Stage { get; set; }


    }
}
