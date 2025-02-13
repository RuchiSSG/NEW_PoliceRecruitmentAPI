using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class DeviceConfigurationDto
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? Id { get; set; }
        public string? RecruitId { get; set; }
        public string? EventId { get; set; }
        public string?  eventName { get; set; }
        public string? categoryId { get; set; }
        public string? UserId { get; set; }
        public string? DeviceId { get; set; }
        public string? Location { get; set; }
        public string? IsActive { get; set; }
        public string? sessionid { get; set; }
        public string? ipaddress { get; set; }


    }
}
