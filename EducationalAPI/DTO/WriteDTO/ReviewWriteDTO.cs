namespace EducationalAPI.DTO.WriteDTO
{
    public class ReviewWriteDTO
    {
        internal int ReviewId { get; set; }
        [Required]
        [MaxLength(128)]
        public string ReviewContents { get; set; }
        [Required]
        [Range(1, 10, ErrorMessage = "Value for score must be a whole number between {1} and {2}.")]
        public int ReviewScore { get; set; }
        internal EduMatNavpointReadDTO EduMatNavpoint { get; set; }

    }
}
