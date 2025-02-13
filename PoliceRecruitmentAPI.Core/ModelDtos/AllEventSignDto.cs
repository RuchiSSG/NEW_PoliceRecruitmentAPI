using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class AllEventSignDto
    {
        public string? UserId { get; set; }
        public string? recConfId { get; set; }
        public BaseModel? BaseModel { get; set; }
        public DataTable? DataTablesign { get; set; }
        public List<AllEventSign>? runningData { get; set; }
        public class AllEventSign
        {
           
            public string? ChestNo { get; set; }

           
            public string? Signature { get; set; }
           
        }
    }
}
