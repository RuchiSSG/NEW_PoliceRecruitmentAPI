using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class CategoryPrirvilegeDto
    {
        public BaseModel? BaseModel { get; set; }
        public string? UserId { get; set; }
        public Guid? Id { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryId { get; set; }
        public string? Isactive { get; set; }
        public string? Is_Admin { get; set; }
         public string?  RecruitId { get; set; }
        public DateTime? Updateddate { get; set; }
        public DateTime? Createddate { get; set; }
        public List<CategoryPrirvilege>? Privilage { get; set; }
        public DataTable? DataTable { get; set; }
        public string? a_id { get; set; }
        public DateTime? a_updateddate { get; set; }
        public DateTime? a_createddate { get; set; }
    }
}
