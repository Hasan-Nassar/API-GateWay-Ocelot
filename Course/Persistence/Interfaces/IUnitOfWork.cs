using System.Threading.Tasks;

namespace Course.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        ICourseRepository CourseRepository { get; }
        Task CompleteAsync();
    }
}