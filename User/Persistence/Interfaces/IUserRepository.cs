/*using System.Threading.Tasks;
using User.Core.Dto;

namespace User.Persistence.Interfaces
{
    public interface IUserRepository:IBaseRepository<Core.Entities.ApplicationUser>
    {
        void Remove(string userId);

        Task<Core.Entities.ApplicationUser> GetById(string id);
        
        Task<PagingDto> GetAll(int? pageIndex, int? pageSize);
    }
}*/