namespace EducationalAPI.DTO.ReadDTO
{
    public class EduMatNavpointReadDTO : WrappedEntity
    {
        internal int EduMatNavpointId { get; set; }
        public string EduMatAuthorName { get; set; }
        public string EduMatTitle { get; set; }
        public string EduMatLocation { get; set; }
        public string EduMatTypeName { get; set; }
        public List<ReviewReadDTO>? EduMatReviews { get; set; }
        public DateTime EduMatTimeCreated { get; set; }
    }
}
