namespace KoperasiTenteraDAL.Repositories
{
    public interface IRepository<TEntity, TEntityId> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(TEntityId entityId);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntityId entityId);
    }
}