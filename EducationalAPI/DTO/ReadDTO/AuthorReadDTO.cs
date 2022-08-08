namespace EducationalAPI.DTO.ReadDTO
{
    public class AuthorReadDTO
    {
        internal int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorDesc { get; set; }
        public List<EduMatNavpointReadDTO>? EduMatNavpoints { get; set; }
        internal int? AmountOfMaterials { get; set; }
    }
}
