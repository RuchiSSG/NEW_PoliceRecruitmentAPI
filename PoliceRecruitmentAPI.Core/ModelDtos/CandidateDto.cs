using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
	public class CandidateDto
	{
		public Guid? Id { get; set; }	
		public BaseModel? BaseModel { get; set; }
		public long? CandidateID { get; set; }
    	public string? UserId { get; set;}
    	public string? Status { get; set;}
    	public string? RecruitmentYear { get; set;}
		public string? OfficeName { get; set; }
		public string? PostName { get; set; }//port
		public string? ApplicationNo { get; set; }
		public string? ExaminationFee { get; set; }//port
		public string? FullNameDevnagari { get; set; }
		public string? FullNameEnglish { get; set; }
        public bool? DocumentUploaded { get; set; }
        public string? MothersName { get; set; }
		public string? Gender { get; set; }
		public string? MaritalStatus { get; set; }
		public string? PassCertificationNo { get; set; }//Candidate
		public DateTime? DOB { get; set; }
		public string? Age { get; set; }
		public string? Address { get; set; }
		public string? PinCode { get; set; }
		public string? MobileNumber { get; set; }
		public string? EmailId { get; set; }
		public string? PermanentAddress { get; set; }
		public string? PermanentPinCode { get; set; }
		public string? Nationality { get; set; }
		public string? Religion { get; set; }
		public string? Cast { get; set; }
		public string? SubCast { get; set; }
		public bool? PartTime { get; set; }
		public bool? ProjectSick { get; set; }
		public bool? ExServiceman { get; set; }//Examination
		public bool? EarthquakeAffected { get; set; }//Attacked
		public bool? Orphan { get; set; }//Attacked
		public bool? Ancestral { get; set; }//Attacked

		public bool? MeasurementStatus { get; set; }
		public decimal? Height { get; set;}
		public decimal? Chest_Inhale { get; set;}
		public decimal? Chest_normal { get; set;}
        public string? ChestNo { get; set; }
        public string? RecruitId{ get; set; }
        public int? groupid { get; set; }
        public string? groupname { get; set; }
        public string? Documentsubmitlater { get; set; }
        public string? allowfromopen { get; set; }
        public string? EventId { get; set; }
        public string? MeasurementRemark { get; set; }
        public string? DocRemark { get; set; }
        public string? Category { get; set; }
        public string?  CategoryId { get; set; }
        public string? CastId { get; set; }
        public DataTable? DataTable { get; set; }
        public DataTable? DataTable1 { get; set; }
        public DataTable? DataTable2 { get; set; }
        public List<Groundtestdata>? Groundtestdata { get; set; }
        public List<GroundTestCategory> Groundtestdata1 { get; set; }
        public class ErrorDetail
        {
            public string Type { get; set; }
            public string Message { get; set; }
        }

    }
    

}
