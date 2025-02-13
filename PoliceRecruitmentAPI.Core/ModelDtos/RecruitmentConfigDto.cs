using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
	public class RecruitmentConfigDto
	{
		public Guid? Id { get; set; }
		public string? post { get; set; }
		public string? place { get; set; }
		public string? year { get; set; }
		public string? UserId { get; set; }
		public BaseModel? BaseModel {  get; set; }

		public DateTime? createdDate { get; set; }
		public DateTime? updatedDate { get; set; }
		public string? isActive { get; set; }

	}
}
