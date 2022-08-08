namespace EducationalAPI.Data.Models
{
    public class EduMatNavpoint
    {
        [Key]
        public int EduMatNavpointId { get; set; }
        public Author? EduMatAuthor { get; set; }
        public string? EduMatTitle { get; set; }
        public string? EduMatLocation { get; set; }
        public EduMatType? EduMatType { get; set; }
        public List<Review>? EduMatReviews { get; set; }
        public DateTime? EduMatTimeCreated { get; set; }

    }
}
