using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Core.Dto;

namespace User.Services.Interface
{
    public interface IUserService
    {
        Task<UserDto> AddUser([FromBody] CreateUserDto userDto);
        Task<string> RemoveUser(string userId);
        Task<UserDto> GetUserById(string userId);
        Task<PagingDto> GetAllUser([FromQuery] int? pageIndex, [FromQuery] int? pageSize);
        
        Task<CreateUserDto> UpdateUser(string id,  CreateUserDto createUserDto);
    }
}
