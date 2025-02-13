using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class DashboardDto
    {
        public BaseModel? BaseModel { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? Staffid { get; set; }
		public string? Staffname { get; set; }
		public string? RoleId { get; set; }
		public string? DutyId { get; set; }
		public string? UserId { get; set; }
         
        public string? RoleName { get; set; }
		public string? DutyName { get; set; }
		public string? RecruitId { get; set; }
		public int? AllCandidate { get; set; }
		public int? ForGround { get; set; }
		public int? ForWriiten { get; set; }
		public int? Pass { get; set; }

       
	}
}
