using EducationalAPI.Data.Models;

namespace EducationalAPI.Profiles
{
    public class ReviewProfiles : Profile
    {
        public ReviewProfiles()
        {
            CreateMap<Review, ReviewReadDTO>()
                    .ForMember(review => review.ReviewContents, reviewDTO => reviewDTO.MapFrom(review => review.ReviewContents))
                    .ForMember(review => review.ReviewScore, reviewDTO => reviewDTO.MapFrom(review => review.ReviewScore))
                    .ForMember(review=>review.EduMatNavpoint, reviewDTO => reviewDTO.MapFrom(review=>review.EduMatNavpoint.EduMatTitle));
            CreateMap<ReviewWriteDTO, Review>()
                .ForMember(authorDTO => authorDTO.ReviewContents, review => review.MapFrom(reviewDTO => reviewDTO.ReviewContents))
                .ForMember(authorDTO => authorDTO.ReviewScore, review => review.MapFrom(reviewDTO => reviewDTO.ReviewScore));
        }
    }
}
