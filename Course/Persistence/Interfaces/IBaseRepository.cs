namespace Course.Persistence.Interfaces
{
    public interface IBaseRepository<in TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}