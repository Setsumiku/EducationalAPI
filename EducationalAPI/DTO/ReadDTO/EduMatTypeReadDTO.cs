namespace EducationalAPI.DTO.ReadDTO
{
    public class EduMatTypeReadDTO : WrappedEntity
    {
        internal int EduMatTypeId { get; set; }
        public string EduMatTypeName { get; set; }
        public string EduMatTypeDesc { get; set; }
    }
}
