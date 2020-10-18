namespace User.Persistence.Interfaces
{
    public interface IBaseRepository <TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}