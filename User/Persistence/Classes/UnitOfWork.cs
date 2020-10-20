using System.Threading.Tasks;
using AutoMapper;
using User.Core.Entities;
using User.Persistence.Context;
using User.Persistence.Interfaces;

namespace User.Persistence.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        public  readonly UserDbContext _context;
     
        public IBaseRepository<ApplicationUser> AccountRepository { get; }
        public UnitOfWork(UserDbContext context)
        {
           
            AccountRepository = new BaseRepository<ApplicationUser>(context);
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}