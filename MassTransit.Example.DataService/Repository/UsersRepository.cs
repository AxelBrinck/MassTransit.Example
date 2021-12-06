using MassTransit.Example.DataService.Data;
using MassTransit.Example.DataService.IRepository;
using MassTransit.Example.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MassTransit.Example.DataService.Repository
{
    public class UsersRepository : GenericRepository<UserEntity>, IUsersRepository
    {
        public UsersRepository(
            AppDbContext context, 
            ILogger logger) : 
                base(context, logger)
        {
        }

        public override async Task<IEnumerable<UserEntity>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "All method failed.", typeof(UsersRepository));
                return new List<UserEntity>();
            }
        }
    }
}