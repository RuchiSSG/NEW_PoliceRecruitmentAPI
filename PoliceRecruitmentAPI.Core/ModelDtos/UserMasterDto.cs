using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
	public class UserMasterDto
	{
		public BaseModel? BaseModel { get; set; }
		public string? UserId { get; set; }
		public Guid? um_id { get; set; }
		public string? um_user_name { get; set; }
        public string[]? um_password { get; set; }
		public string? um_staffname { get; set; }

		public string? um_post { get; set; }
		public string? um_postname { get; set; }
		public string? um_duty { get; set; }
		public string? um_dutyname { get; set; }
		public string? um_bukkel_no { get; set; }
		
		public string? um_phone_no { get; set; }
		
		
		public string? um_isactive { get; set; }
		public string? um_roleid { get; set; }
		public string? s_umids { get; set; }

		public string? um_rolename { get; set; }
		public DateTime? um_createddate { get; set; }
		public DateTime? um_updateddate { get; set; }

        public string? um_recruitid { get; set; }
        public DataTable? DataTable { get; set; }
	}
}
