using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class RFIDChestNoMappingDto
    {
        public BaseModel? BaseModel { get; set; }
        public string? Id { get; set; }
        public string? RFID { get; set; }
        public string? UserId { get; set; }
        public string? ChestNo { get; set; }
        public string? CandidateID { get; set; }
        public DateTime? CreatedDate { get; set; }
		public string? eventId { get; set; }
		public string? eventName { get; set; }
		public string? Position { get; set; }
		public string? DeviceName { get; set; }
		public string? RecruitId { get; set; }
        public string? Barcode { get; set; }
        public string? currentDateTime { get; set; }
        public DataTable? DataTable1 { get; set; }
        public DataTable? DataTable { get; set; }
        public string?  sessionid { get; set; }
        public string? ipaddress { get; set; }
        public string? LapCount { get; set; }
    }
}
