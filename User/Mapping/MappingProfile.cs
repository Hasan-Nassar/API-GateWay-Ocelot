using AutoMapper;
using User.Core.Dto;

namespace User.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Entities.User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, Core.Entities.User>().ReverseMap();
        }
    }
}