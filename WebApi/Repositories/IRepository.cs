
using WebApi.Entities;

namespace WebApi.Repositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class
        where TKey : struct
    {
        Task AddAsync(TEntity companyToCreate);
        IQueryable<TEntity> GetAll();
        Task<TEntity?> GetAsync(TKey key);
        
    }
}