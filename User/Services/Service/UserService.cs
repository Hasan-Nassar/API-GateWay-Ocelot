using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using User.Core.Dto;
using User.Core.Entities;
using User.Persistence.Interfaces;
using User.Services.Interface;

namespace User.Services.Service
{
    public class UserService : IUserService
    {
        /*private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      

        public UserService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
          
        }
        public async Task<UserDto> AddUser([FromBody] CreateUserDto userDto)
        {
           
            
            var user = _mapper.Map<CreateUserDto,Core.Entities.ApplicationUser>(userDto);
            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Core.Entities.ApplicationUser, UserDto>(user);
            result.UserName = user.UserName;
            return result;
        }
        
        public async Task<string> RemoveUser(string userId)
        {
            _unitOfWork.UserRepository.Remove(userId);
            await _unitOfWork.CompleteAsync();
            return "accept deleted";

        }
        
        public async Task<UserDto> GetUserById(string userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var p = _mapper.Map<Core.Entities.ApplicationUser, UserDto>(user);
            p.UserName = user.UserName;
            return p;
        }

        public async Task<PagingDto> GetAllUser([FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {

            var user = await _unitOfWork.UserRepository.GetAll(pageIndex, pageSize);
            return user;
        }
        
        public async Task<CreateUserDto> UpdateUser(string id, CreateUserDto userDto)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            if (user == null)
                throw new Exception("Not Found... ");
            var users = _mapper.Map(userDto, user);
            _unitOfWork.UserRepository.Update(users);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<Core.Entities.ApplicationUser, CreateUserDto>(users);
        }*/
         private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration config, IUnitOfWork unitOfWork)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<object> Login(LoginDto loginDto)
        {
            var islogin = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.PasswordHash, false, false);
            if (!islogin.Succeeded) throw new Exception("Invalid Username or Password! ");
           
                var result = new { Token = await GenerateJSONWebTokenAsync(loginDto) };
                return result;
        }

        public async Task<object> AssignRoles(AssignRoleDto assignRoleDto)
        {
            var user = await _userManager.FindByIdAsync(assignRoleDto.UserId);

            if (user == null || assignRoleDto.Role == null)
                throw new Exception("User Not Found! ");
            if (!(await _userManager.IsInRoleAsync(user, assignRoleDto.Role)))
            {
                var result = await _userManager.AddToRoleAsync(user, assignRoleDto.Role);
                return result;
            }
            else throw new Exception("Role Already Assigned! ");
        }

        public async Task<object> Logout()
        {
            await _signInManager.SignOutAsync();
            return "Logged out Succefully";
        }

        public async Task<object> Register(RegisterDto signUpDto)
        {
            try
            {
                var user = new ApplicationUser { UserName = signUpDto.UserName, Email = signUpDto.Email };
                var result = await _userManager.CreateAsync(user, signUpDto.Password);
                if (!result.Succeeded) throw new Exception("Register not completed ");
                return result;
            }
            catch (Exception ex)
            {
                return new { message = ex.Message };
            }
        }

        public async Task<PagingDto<LoginDto>> GetPagedUsers(int? pageIndex, int? pageSize)
        {
            PagingDto<ApplicationUser> user = await _unitOfWork.AccountRepository.GetAllIncludedPagnation(U => U.UserName != null, pageIndex, pageSize);
            PagingDto<LoginDto> result = _mapper.Map<PagingDto<ApplicationUser>, PagingDto<LoginDto>>(user);
            return result;
        }

       

        public async Task<LoginDto> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new Exception("User Not Found! ");
            var result = _mapper.Map<ApplicationUser, LoginDto>(user);
            return result;
        }

        private async Task<string> GenerateJSONWebTokenAsync(LoginDto loginDto )
        {

            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:SecurityKey"])), SecurityAlgorithms.HmacSha256);

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            //var userClaims = await _userManager.GetClaimsAsync(user);
            // var roles = await _userManager.GetRolesAsync(user);
           foreach(var role in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }
         
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(type: "Username", user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.Ticks.ToString(), ClaimValueTypes.Integer64)
            }.Union(userClaims);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}