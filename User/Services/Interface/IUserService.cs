using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Core.Dto;

namespace User.Services.Interface
{
    public interface IUserService
    {
        /*Task<UserDto> AddUser([FromBody] CreateUserDto userDto);
        Task<string> RemoveUser(string userId);
        Task<UserDto> GetUserById(string userId);
        Task<PagingDto> GetAllUser([FromQuery] int? pageIndex, [FromQuery] int? pageSize);
        Task<CreateUserDto> UpdateUser(string id,  CreateUserDto createUserDto);*/
        
        Task<PagingDto<LoginDto>> GetPagedUsers(int? pageIndex, int? pageSize);
        Task<object> Login(LoginDto loginDto);
        Task<object> Register(RegisterDto signUpDto);
        Task<object> Logout();
        //Task<List<LoginDto>> GetAllUsers();
        Task<LoginDto> GetUser(string id);
        Task<object> AssignRoles(AssignRoleDto assignRoleDto);
    }
}
