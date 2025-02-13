using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
	public class DutyMasterDto
	{
		public BaseModel? BaseModel { get; set; }
		public string? UserId { get; set; }
		public Guid?d_id { get; set; }
		public string? d_dutyname { get; set; }
		public string?d_description { get; set; }
		public string?d_module { get; set; }
		public string?d_isactive { get; set; }
		public string?d_is_Admin { get; set; }
		public string? d_no_of_user { get; set; }
        public string? d_recruitid { get; set; }
        public DateTime?d_updateddate { get; set; }
		public DateTime?d_createddate { get; set; }
		public List<DutyPrivilege>? Privilage { get; set; }
		public DataTable? DataTable { get; set; }
		public string? a_id { get; set; }
		public DateTime? a_updateddate { get; set; }
		public DateTime? a_createddate { get; set; }
	}
}
