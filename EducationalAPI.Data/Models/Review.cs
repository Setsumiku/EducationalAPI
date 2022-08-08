namespace EducationalAPI.Data.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string? ReviewContents { get; set; }
        [Range(1,10, ErrorMessage ="Value for score must be a whole number between {1} and {2}.")]
        public int ReviewScore { get; set; }
        public EduMatNavpoint? EduMatNavpoint { get; set; }
    }
}
