using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class OmrMasterDto
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? Id { get; set; }
        public string? UserId { get; set; }
        public string? RecruitId { get; set; }
        public string? AnswerData { get; set; }
        public string? IsActive { get; set; }
    }
}
