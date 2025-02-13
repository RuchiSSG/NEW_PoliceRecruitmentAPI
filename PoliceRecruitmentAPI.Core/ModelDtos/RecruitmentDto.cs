using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
	public class RecruitmentDto
	{
		public Guid? Id { get; set; }
		public string? post { get; set; }
		public string? place { get; set; }
        public string? noofseats { get; set; }
        public string? year { get; set; }
		public string? UserId { get; set; }
		public BaseModel? BaseModel {  get; set; }
        public string?  UserName { get; set; }
        public string? Password { get; set; }
        public DateTime? createdDate { get; set; }
		public DateTime? updatedDate { get; set; }
		public string? isActive { get; set; }
        public DateTime? startDate { get; set; }
        public string? noOfCandidate { get; set; }
        public string? RecruitId { get; set; }


    }
}
