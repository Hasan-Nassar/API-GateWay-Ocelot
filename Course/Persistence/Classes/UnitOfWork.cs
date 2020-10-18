using System.Threading.Tasks;
using AutoMapper;
using Course.Persistence.Interfaces;

namespace Course.Persistence.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CourseDbContext _context;
        


        public UnitOfWork(CourseDbContext context)
        {
            _context = context;
            CourseRepository = new CourseRepository(context);
        }


        public ICourseRepository CourseRepository { get; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}