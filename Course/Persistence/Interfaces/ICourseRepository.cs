using System.Threading.Tasks;
using Course.Core.Dto;

namespace Course.Persistence.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Core.Entity.Course>
    {
        void Remove(int courseId);

        Task<Core.Entity.Course> GetById(int id);

        Task<PagingDto> GetAll(int? pageIndex, int? pageSize);
    }
}
