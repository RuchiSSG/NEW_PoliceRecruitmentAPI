
namespace PoliceRecruitmentAPI.Core.ModelDtos
{
    public class ParameterMasterDto
    { 
        public BaseModel? BaseModel { get; set; }
        public string? UserId { get; set; }
        public Guid? p_id { get; set; }
        public string? p_parametername { get; set; }
        public string? p_code { get; set; }
        public string? p_isactive { get; set; }
        public DateTime? p_createddate { get; set; }
        public DateTime? p_updateddate { get; set; }

    }
}
