namespace EducationalAPI.DTO.WriteDTO
{
    public class AuthorWriteDTO
    {
        internal int AuthorId { get; set; }
        [Required(ErrorMessage = "Name cannot be empty")]
        [MaxLength(64)]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Description cannot be empty")]
        [MaxLength(128)]
        public string AuthorDesc { get; set; }
        internal int? AmountOfMaterials { get; set; }
    }
}
