using System.Threading.Tasks;
using User.Persistence.Classes;

namespace User.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }
        Task CompleteAsync();
    }
}