using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class ScanningdocDto
    {
        public BaseModel? BaseModel { get; set; }
        public long? CandidateID { get; set; }
        public string? UserId { get; set; }
        public string? ChestNo { get; set; }
        public string? Thumbstring { get; set; }
        public string? Imagestring { get; set; }
        public DateTime? Date { get; set; }
		public string? RecruitId { get; set; }
        public string?  CategoryName { get; set; }
        public string? Thumbstring1 { get; set; }
        public string? Thumbstring2 { get; set; }
        public string? Thumbstring3 { get; set; }
        public string? Thumbstring4 { get; set; }
        public string? Thumbstring5 { get; set; }
        public string? Thumbstring6 { get; set; }
        public string? Thumbstring7 { get; set; }
        public string? Thumbstring8 { get; set; }
        public string? Thumbstring9 { get; set; }
        public string? IVImage { get; set; }
        public string? Secretkeys { get; set; }


    }
}
