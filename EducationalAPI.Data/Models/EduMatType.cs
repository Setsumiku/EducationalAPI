namespace EducationalAPI.Data.Models
{
    public class EduMatType
    {
        [Key]
        public int EduMatTypeId { get; set; }
        public string? EduMatTypeName { get; set; }
        public string? EduMatTypeDesc { get; set; }
    }
}
