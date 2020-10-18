using System.Threading.Tasks;
using User.Core.Dto;

namespace User.Persistence.Interfaces
{
    public interface IUserRepository:IBaseRepository<Core.Entities.User>
    {
        void Remove(string userId);

        Task<Core.Entities.User> GetById(string id);
        
        Task<PagingDto> GetAll(int? pageIndex, int? pageSize);
    }
}