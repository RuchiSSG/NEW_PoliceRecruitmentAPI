using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class CategoryMasterDto
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? Id { get; set; }
        public string? categoryId { get; set; }
        public string? recConfId { get; set; }
        public string? UserId { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? updatedDate { get; set; }
      
        
        public string? IsActive { get; set; }
        public DataTable? DataTable { get; set; }

        public List<Categoryins> Categoryins { get; set; }
    }

    public class Categoryins     {
       
        public string? CategoryName { get; set; }
        
    }

}
