using MassTransit.Example.DataService.IConfiguration;
using MassTransit.Example.DataService.IRepository;
using MassTransit.Example.DataService.Repository;
using Microsoft.Extensions.Logging;

namespace MassTransit.Example.DataService.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        
        public IUsersRepository UserRepository { get; }

        public UnitOfWork(AppDbContext context, ILoggerFactory logger)
        {
            _context = context;
            _logger = logger.CreateLogger("db_logs");

            UserRepository = new UsersRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}