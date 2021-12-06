using MassTransit.Example.DataService.Data;
using MassTransit.Example.DataService.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MassTransit.Example.DataService.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        internal readonly DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(
            AppDbContext context,
            ILogger logger)
        {
            _context = context;
            dbSet = context.Set<T>();
            _logger = logger;
        }

        public virtual async Task<bool> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetById(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}