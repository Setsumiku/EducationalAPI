namespace EducationalAPI.Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserRole { get; set; }
        public int UserLogin { get; set; }
        public string UserPassword { get; set; }
    }
}
