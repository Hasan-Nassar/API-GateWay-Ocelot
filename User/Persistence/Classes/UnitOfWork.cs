using System.Threading.Tasks;
using AutoMapper;
using User.Persistence.Context;
using User.Persistence.Interfaces;

namespace User.Persistence.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _context;
        
        public UnitOfWork(UserDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(context);
        }


        public UserRepository UserRepository { get; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}