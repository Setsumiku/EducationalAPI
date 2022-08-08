namespace EducationalAPI.Data.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorDesc { get; set; }
        public List<EduMatNavpoint>? EduMatNavpoints { get; set; }
        public int? AmountOfMaterials { get; set; } = 0;

        public Author()
        {
            AmountOfMaterials++;
        }
    }
}
