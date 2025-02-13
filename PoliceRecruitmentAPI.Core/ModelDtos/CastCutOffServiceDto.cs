using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class CastCutOffServiceDto
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? Id { get; set; }
        public string? UserId { get; set; }
        public string? RecruitId { get; set; }
        public string? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Selectionratio { get; set; }
        public string? Groundtestcutoff { get; set; }
        public string? Writtentestcutoff { get; set; }
        public string? IsActive { get; set; }
        public DataTable? DataTable { get; set; }
        public List<Casts> Castsdata { get; set; }
    }
    public class Casts
        { 
        public string? CutOffPosition { get; set; }
        public string? SubCategoryName { get; set; }
        public string? ParentCastId { get; set; }
        public string? Total { get; set; }
        public string?  CastTotal { get; set; }
        //public string? NTC { get; set; }
        //public string? SBC { get; set; }
        //public string? Open { get; set; }

        //public string? NTB { get; set; }
        //public string? NTD { get; set; }
        //public string? VJ { get; set; }
    }
   
}
