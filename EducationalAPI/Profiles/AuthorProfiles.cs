using EducationalAPI.Data.Models;

namespace EducationalAPI.Profiles
{
    public class AuthorProfiles : Profile
    {
        public AuthorProfiles()
        {
            CreateMap<Author, AuthorReadDTO>()
                    .ForMember(author => author.AuthorName, authorDTO => authorDTO.MapFrom(author => author.AuthorName))
                    .ForMember(author => author.AuthorDesc, authorDTO => authorDTO.MapFrom(author => author.AuthorDesc))
                    .ForMember(author => author.EduMatNavpoints, authorDTO => authorDTO.MapFrom(author => author.EduMatNavpoints));
            CreateMap<AuthorWriteDTO, Author>()
                .ForMember(authorDTO => authorDTO.AuthorName, author => author.MapFrom(authorDTO => authorDTO.AuthorName))
                .ForMember(authorDTO => authorDTO.AuthorDesc, author => author.MapFrom(authorDTO => authorDTO.AuthorDesc));
        }
    }
}
