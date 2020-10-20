using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Core.Dto;
using User.Persistence.Context;
using User.Persistence.Interfaces;

namespace User.Persistence.Classes
{
   
    public class BaseRepository<TEntity>: IBaseRepository <TEntity> where TEntity : class
    {
        /*private readonly DbContext _context;

        
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
            _context.Entry(entity).State = EntityState.Modified;*/
        private readonly UserDbContext _context = null;
        private readonly DbSet<TEntity> table = null;
     
        public BaseRepository(UserDbContext context) 
        {
            _context = context;
            table = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null, string Includes = null)
        {
            if(predicate != null)
            {
            var query = table.Where(predicate);
                if(Includes !=null)
                {
                    foreach (var str in Includes.Split(","))
                        query = query.Include(str).AsQueryable();
                }
                return await query.ToListAsync();
            }
            return await table.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllIncluded(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] Includes)
        {
            var query = table.Where(predicate);
            foreach (var Include in Includes)
            {
                query = query.Include(Include);
            }
            return await query.ToListAsync();
        }

        public async Task<PagingDto<TEntity>> GetAllIncludedPagnation(Expression<Func<TEntity, bool>> predicate = null, int? pageIndex = 1, int? pageSize = 10, params Expression<Func<TEntity, object>>[] Includes)
        {
            if (pageIndex <= 0) pageIndex = 1;
            if (pageSize <= 0) pageSize = 10;

            var query = table.Where(predicate);
            foreach (var Include in Includes)
            {
                query = query.Include(Include);
            }

            return new PagingDto<TEntity>
            {
                TotalCount = await query.CountAsync(),
                Result = await query.Skip((int)((pageIndex - 1) * pageSize)).Take((int)pageSize).ToListAsync()
            };
        }

        public async Task<TEntity> GetById(object id)
        { 
            return await table.FindAsync(id);
        }

        public void Add(TEntity obj)
        { 
            table.Add(obj); 
        }

        public void Update(TEntity obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public TEntity Delete(TEntity existing)
        {
            table.Remove(existing);
            return existing;
        }
        
        public Task DeleteRange(List<TEntity> entites)
        {
            table.RemoveRange(entites);
            return Task.CompletedTask;
        }
    }
}