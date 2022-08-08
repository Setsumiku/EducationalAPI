using EducationalAPI.Data.Models;

namespace EducationalAPI.Profiles
{
    public class EduMatTypeProfiles : Profile
    {
        public EduMatTypeProfiles()
        {
            CreateMap<EduMatType, EduMatTypeReadDTO>()
                    .ForMember(eduMatType => eduMatType.EduMatTypeName, eduMatTypeDTO => eduMatTypeDTO.MapFrom(eduMatType => eduMatType.EduMatTypeName))
                    .ForMember(eduMatType => eduMatType.EduMatTypeDesc, eduMatTypeDTO => eduMatTypeDTO.MapFrom(eduMatType => eduMatType.EduMatTypeDesc));
            CreateMap<EduMatTypeWriteDTO, EduMatType>()
                .ForMember(eduMatTypeDTO => eduMatTypeDTO.EduMatTypeDesc, eduMatType => eduMatType.MapFrom(eduMatTypeDTO => eduMatTypeDTO.EduMatTypeDesc))
                .ForMember(eduMatTypeDTO => eduMatTypeDTO.EduMatTypeName, eduMatType => eduMatType.MapFrom(eduMatTypeDTO => eduMatTypeDTO.EduMatTypeName));
        }
    }
}
