using System.Threading.Tasks;
using User.Core.Entities;
using User.Persistence.Classes;

namespace User.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<ApplicationUser> AccountRepository { get; }
        Task CompleteAsync();
    }
}