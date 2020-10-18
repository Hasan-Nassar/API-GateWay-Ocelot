using Course.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Course.Persistence.Classes
{
    public class BaseRepository<TEntity> : IBaseRepository <TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }


    }
}