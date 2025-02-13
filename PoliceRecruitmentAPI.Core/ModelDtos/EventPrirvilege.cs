using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class EventPrirvilege
    {
         public string? a_roleid { get; set; }
        public string? a_menuid { get; set; }
        public string? Isactive { get; set; }
        public string? addaccess { get; set; }
        public string? editaccess { get; set; }
        public string? deleteaccess { get; set; }
        public string? viewaccess { get; set; }
        public string? workflow { get; set; }
    }
}
