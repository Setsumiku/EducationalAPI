namespace EducationalAPI.DTO
{
    public class UserLoginDTO
    {
        internal int UserId { get; set; }
        [Required]
        public string UserLogin { get; set; }
        [Required]
        public string UserPassword { get; set; }
        internal string UserRole { get; set; }
    }
}
