namespace EducationalAPI.DTO.ReadDTO
{
    public class ReviewReadDTO
    {
        internal int ReviewId { get; set; }
        public string ReviewContents { get; set; }
        public int ReviewScore { get; set; }
        public EduMatNavpointReadDTO EduMatNavpoint { get; set; }

    }
}
