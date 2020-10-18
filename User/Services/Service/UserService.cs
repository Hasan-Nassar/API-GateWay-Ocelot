using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using User.Core.Dto;
using User.Persistence.Interfaces;
using User.Services.Interface;

namespace User.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      

        public UserService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
          
        }
        public async Task<UserDto> AddUser([FromBody] CreateUserDto userDto)
        {
            var user = _mapper.Map<CreateUserDto,Core.Entities.User>(userDto);
            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<Core.Entities.User, UserDto>(user);
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
            var p = _mapper.Map<Core.Entities.User, UserDto>(user);
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
            return _mapper.Map<Core.Entities.User, CreateUserDto>(users);
        }
    }
}