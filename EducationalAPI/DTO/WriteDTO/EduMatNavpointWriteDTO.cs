namespace EducationalAPI.DTO.WriteDTO
{
    public class EduMatNavpointWriteDTO
    {
        internal int EduMatNavpointId { get; set; }
        [Required(ErrorMessage = "Author's Id field cannot be empty")]
        public int AuthorAuthorId { get; set; }
        [Required(ErrorMessage = "Title cannot be empty")]
        [MaxLength(64)]
        public string EduMatTitle { get; set; }
        [Required(ErrorMessage = "Location cannot be empty")]
        [MaxLength(64)]
        public string EduMatLocation { get; set; }
        [Required(ErrorMessage = "Type's Id field cannot be empty")]
        public int EduMatTypeEduMatTypeId { get; set; }
        internal DateTime? EduMatTimeCreated { get; set; } = DateTime.Now;
    }
}
