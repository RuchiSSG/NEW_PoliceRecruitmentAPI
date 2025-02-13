using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
	public class RecruitmentEventDto
	{
		public Guid? Id { get; set; }
		public string? UserId { get; set; }
		public BaseModel? BaseModel { get; set; }

		public DateTime? createdDate { get; set; }
		public DateTime? updatedDate { get; set; }
		public string? isActive { get; set; }
		public string? eventName { get; set; }
        public string?  EventsId { get; set; }
        public string? eventUnit { get; set; }
		
		public string? groupid { get; set; }
        public string?  categoryId { get; set; }
        public string? recConfId { get; set; }
		public string? sessionid { get; set; }
		public string? ipaddress { get; set; }
		public DataTable? DataTable { get; set; }
       
      
        

        public List<RecruitmentConfig> RecruitmentConfig { get; set; }

	}
	public class RecruitmentConfig
	{
        public string? minValue { get; set; }
        public string? maxValue { get; set; }
        public string? score { get; set; }
		public string? gender { get; set; }
        public string? category { get; set; }
        

    }

	//public class AllEventSign
	//{
 //       //public string? CandidateName { get; set; }
 //       //public string? CandidateID { get; set; }
 //       public string? ChestNo { get; set; }

 //       //public string? eightmeter { get; set; }

 //       //public string? hundredmeter { get; set; }

 //       //public string? sixteenhundredmeter { get; set; }

 //       //public string? Shotput { get; set; }
 //       public string? Signature { get; set; }
 //       //public string? Date { get; set; }
 //      // public string? recConfId { get; set; }
 //      // public string? UserId { get; set; }
 //   }
}
