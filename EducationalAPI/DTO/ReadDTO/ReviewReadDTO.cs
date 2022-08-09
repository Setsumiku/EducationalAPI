namespace EducationalAPI.DTO.ReadDTO
{
    public class ReviewReadDTO : WrappedEntity
    {
        internal int ReviewId { get; set; }
        public string ReviewContents { get; set; }
        public int ReviewScore { get; set; }
        public string EduMatNavpoint { get; set; }

    }
}
