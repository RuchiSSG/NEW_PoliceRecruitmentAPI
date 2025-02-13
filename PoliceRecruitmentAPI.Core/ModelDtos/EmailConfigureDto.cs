using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
	public class EmailConfigureDto
	{
		public BaseModel? BaseModel { get; set; }
		public Guid? id { get; set; }
		public string? email { get; set; }
		public string? password { get; set; }
		public string? smtp_server { get; set; }
		public int? smtp_port { get; set; }
	}
}
