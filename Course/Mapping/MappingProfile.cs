using AutoMapper;
using Course.Core.Dto;

namespace Course.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Entity.Course, CourseDto>().ReverseMap();
        }
    }
}

