using EducationalAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationalAPI.Data.Context
{
    public class EducationalAPIContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<EduMatType> EduMatTypes { get; set; }
        public DbSet<EduMatNavpoint> EduMatNavpoints { get; set; }

        public EducationalAPIContext(DbContextOptions<EducationalAPIContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EduMatNavpoint>(
            entity =>
            {
                entity.HasOne(d => d.EduMatAuthor)
                    .WithMany(p => p.EduMatNavpoints)
                    .HasForeignKey("AuthorId");
                entity.HasOne(d => d.EduMatType)
                    .WithMany()
                    .HasForeignKey("EduMatTypeId");
                entity.HasMany(d => d.EduMatReviews)
                    .WithOne(p => p.EduMatNavpoint);
            });
            builder.Entity<User>().HasData(
                new { UserId = 1, UserRole = "Admin", UserLogin = "admin", UserPassword = "admin" },
                new { UserId = 2, UserRole = "User", UserLogin = "user", UserPassword = "user" }
                );
            builder.Entity<Review>().HasData(
                new { ReviewId = 1, ReviewContents = "Very nice", ReviewScore = 5 },
                new { ReviewId = 2, ReviewContents = "Very sad", ReviewScore = 1 },
                new { ReviewId = 3, ReviewContents = "Sad", ReviewScore = 3 },
                new { ReviewId = 4, ReviewContents = "Nice", ReviewScore = 7 },
                new { ReviewId = 5, ReviewContents = "Based", ReviewScore = 9 }
                );
            builder.Entity<EduMatType>().HasData(
                new { EduMatTypeId = 1, EduMatTypeName = "Video", EduMatTypeDesc = "It's a video." },
                new { EduMatTypeId = 2, EduMatTypeName = "Article", EduMatTypeDesc = "It's a written article." },
                new { EduMatTypeId = 3, EduMatTypeName = "DT", EduMatTypeDesc = "It's a type of knowledge transfer directly into a brain" },
                new { EduMatTypeId = 4, EduMatTypeName = "Podcast", EduMatTypeDesc = "Long form audio." }
                );
            builder.Entity<Author>().HasData(
                new { AuthorId = 1, AuthorTypeName = "Dominik", AuthorDesc = "Mentor and programmer" },
                new { AuthorId = 2, AuthorTypeName = "Youtuber", AuthorDesc = "Some random guy with a webcam" },
                new { AuthorId = 3, AuthorTypeName = "Dog", AuthorDesc = "Mood booster and lifestyle coach" },
                new { AuthorId = 4, AuthorTypeName = "Todd Howard", AuthorDesc = "It just works" }
                );
            builder.Entity<EduMatNavpoint>().HasData(
                new { EduMatNavpointId = 1, AuthorId = 4, EduMatTypeId = 2, EduMatTitle = "Yes man", EduMatLocation = "My ssd", EduMatTimeCreated = DateTime.Now },
                new { EduMatNavpointId = 2, AuthorId = 1, EduMatTypeId = 2, EduMatTitle = "No man", EduMatLocation = "Web", EduMatTimeCreated = DateTime.Now },
                new { EduMatNavpointId = 3, AuthorId = 4, EduMatTypeId = 1, EduMatTitle = "Cat video", EduMatLocation = "Web", EduMatTimeCreated = DateTime.Now },
                new { EduMatNavpointId = 4, AuthorId = 3, EduMatTypeId = 3, EduMatTitle = "Dog teaches programming", EduMatLocation = "Lost", EduMatTimeCreated = DateTime.Now },
                new { EduMatNavpointId = 5, AuthorId = 1, EduMatTypeId = 4, EduMatTitle = "Coincidence", EduMatLocation = "Stolen", EduMatTimeCreated = DateTime.Now }
                );
        }
    }
}
