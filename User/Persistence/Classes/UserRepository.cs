using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using User.Core.Dto;
using User.Persistence.Context;
using User.Persistence.Interfaces;

namespace User.Persistence.Classes
{
    public class UserRepository:BaseRepository<Core.Entities.User> , IUserRepository
    {
        private readonly UserDbContext _context;
      
        public UserRepository(UserDbContext context) 
            :base(context)
        {
            _context = context;
           
        }
        
        public void Remove(string id) 
        {
            var user = _context.Users.Find(id);
            if (user == null)
                throw new Exception("User Not Found! ");
            _context.Users.Remove(user);
          
        }
        
        public async Task<Core.Entities.User> GetById(string userId)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Id == userId);
            return user;

        }
        
        public async Task<PagingDto> GetAll(int? pageIndex, int? pageSize)
        {
            
            PagingDto result = new PagingDto();
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageIndex = 10;
            
            result.TotalCount= await _context.Users.CountAsync();
            var query = _context.Users.Select(w=>
                new UserDto()
                {
                    Id = w.Id,
                    UserName = w.UserName,
                    PasswordHash = w.PasswordHash,
                    Email = w.Email
                    
                }).Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
            
            result.Result =await query.ToListAsync();
            return result;

        }
    }
}