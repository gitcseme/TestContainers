using Microsoft.EntityFrameworkCore;
using WebApi.EFConfig;
using WebApi.Entities;

namespace WebApi.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
        where TKey : struct
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<TEntity> _table;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetAsync(TKey key)
        {
            return await _table.FindAsync(key);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _table;
        }

        public async Task AddAsync(TEntity companyToCreate)
        {
            await _table.AddAsync(companyToCreate);
            await _context.SaveChangesAsync();
        }
    }
}
