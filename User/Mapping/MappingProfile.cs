using AutoMapper;
using User.Core.Dto;
using User.Core.Entities;

namespace User.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap(typeof(PagingDto<>), typeof(PagingDto<>));
            CreateMap<PagingDto<ApplicationUser>, PagingDto<LoginDto>>();
            CreateMap<Core.Entities.ApplicationUser, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, Core.Entities.ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, LoginDto>();
        }
    }
}