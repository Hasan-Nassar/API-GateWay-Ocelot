
/*
using System.Threading.Tasks;
using User.Core.Dto;
using User.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace User.Controllers
{

    [ApiController]
    [Route("/[Controller]")]
    public class UserController : Controller
    {
            private readonly IUserService _userService;
         
    
            public UserController( IUserService userService)
            {
                _userService = userService;
               
            }

            /// <summary>
            /// Add New User
            /// </summary>
            /// <param name="createUserDto"></param>
            /// <returns></returns>
            [HttpPost("Add User")]
            public async Task<IActionResult> AddUser([FromBody] CreateUserDto createUserDto) => Ok(await _userService.AddUser(createUserDto));
            /// <summary>
            /// Remove a User
            /// </summary>
            /// <param name="userId"></param>
            /// <returns></returns>
            [HttpDelete("Delete User")]
            public async Task<IActionResult> RemoveUser(string userId) => Ok(await _userService.RemoveUser(userId));
            /// <summary>
            /// Get User By Id
            /// </summary>
            /// <param name="userId"></param>
            /// <returns></returns>
            [HttpGet("GetUserById")]
            public async Task<IActionResult> GetUser(string userId) => Ok(await _userService.GetUserById(userId));
            /// <summary>
            /// Get All User
            /// </summary>
            /// <returns></returns>
            [HttpGet("GetAllUsers")]
            public async Task<IActionResult> GetAllUser([FromQuery] int? pageIndex, [FromQuery] int? pageSize) => Ok(await _userService.GetAllUser(pageIndex, pageSize));
            /// <summary>
            /// EditUser
            /// </summary>
            /// <param name="id"></param>
            /// <param name="createUserDto"></param>
            /// <returns></returns>
            [HttpPut("{UserId}")]
            public async Task<IActionResult> UpdatePost(string id, [FromBody] CreateUserDto createUserDto) => Ok(await _userService.UpdateUser(id, createUserDto));
        
        
    }
}*/