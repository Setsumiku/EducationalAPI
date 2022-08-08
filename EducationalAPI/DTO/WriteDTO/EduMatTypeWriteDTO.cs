namespace EducationalAPI.DTO.WriteDTO
{
    public class EduMatTypeWriteDTO
    {
        internal int EduMatTypeId { get; set; }
        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(32)]
        public string EduMatTypeName { get; set; }
        [Required(ErrorMessage = "Description cannot be empty")]
        [MaxLength(128)]
        public string EduMatTypeDesc { get; set; }
    }
}
