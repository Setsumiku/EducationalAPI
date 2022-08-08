using EducationalAPI.Data.Models;

namespace EducationalAPI.Profiles
{
    public class EduMatNavpointProfiles : Profile
    {
        public EduMatNavpointProfiles()
        {
            CreateMap<EduMatNavpoint,EduMatNavpointReadDTO>()
                    .ForMember(navpoint => navpoint.EduMatAuthorName, navpointDto => navpointDto.MapFrom(navpoint => navpoint.EduMatAuthor.AuthorName))
                    .ForMember(navpoint => navpoint.EduMatTypeName, navpointDto => navpointDto.MapFrom(navpoint => navpoint.EduMatType.EduMatTypeName))
                    .ForMember(navpoint => navpoint.EduMatTitle, navpointDto => navpointDto.MapFrom(navpoint => navpoint.EduMatTitle))
                    .ForMember(navpoint => navpoint.EduMatLocation, navpointDto => navpointDto.MapFrom(navpoint => navpoint.EduMatLocation))
                    .ForMember(navpoint => navpoint.EduMatTimeCreated, navpointDto => navpointDto.MapFrom(navpoint => navpoint.EduMatTimeCreated))
                    .ForMember(navpoint => navpoint.EduMatReviews, navpointDto => navpointDto.MapFrom(navpoint => navpoint.EduMatReviews));
            CreateMap<EduMatNavpointWriteDTO, EduMatNavpoint>()
                    .ForMember(navpoint => navpoint.EduMatAuthor.AuthorId, navpointDto => navpointDto.MapFrom(navpoint => navpoint.EduMatAuthorId))
                    .ForMember(navpoint => navpoint.EduMatType.EduMatTypeId, navpointDto => navpointDto.MapFrom(navpoint => navpoint.EduMatTypeId))
                    .ForMember(navpoint => navpoint.EduMatTitle, navpointDto => navpointDto.MapFrom(navpoint => navpoint.EduMatTitle))
                    .ForMember(navpoint => navpoint.EduMatLocation, navpointDto => navpointDto.MapFrom(navpoint => navpoint.EduMatLocation));
        }
    }
}
