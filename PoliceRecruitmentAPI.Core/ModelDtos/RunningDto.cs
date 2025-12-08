using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
	public class running
	{

		public string? CandidateId { get; set; }
		public string? ChestNo { get; set; }
		public TimeSpan? StartTime { get; set; }
		public TimeSpan? EndTime { get; set; }
		public string? Group { get; set; }
		public string? Duration { get; set; }
        public string? distance1 { get; set; }
        public string? distance2 { get; set; }
        public string? distance3 { get; set; }
        public string? Signature { get; set; }
		public DateTime? Date { get; set; }
        public string? Eventid { get; set; }
        public string? GrpLdrSignature { get; set; }
        public string? InchargeSignature { get; set; }
    }
	public class RunningDto
	{
		public string? Id { get; set; }
		public List<running>? runningData { get; set; }
		public DataTable? DataTable { get; set; }

		public BaseModel? BaseModel { get; set; }
		public long? CandidateID { get; set; }
		public string? UserId { get; set; }
		public string? ChestNo { get; set; }
		public TimeSpan? StartTime { get; set; }
		public TimeSpan? EndTime { get; set; }
		public string? Group { get; set; }
		public string? NoOfAttemt { get; set; }
		public string? Duration { get; set; }
		public string? Signature { get; set; }
		public string? Score { get; set; }
		public DateTime? Date { get; set; }
        public string? Eventid { get; set; }
        public string? AddGrpLdrName { get; set; }
        public string? GrpLdrName { get; set; }
        public string? GrpLdrSignature { get; set; }
        public string? InchargeSignature { get; set; }
        public string? RecruitId { get; set; }

	}
}
