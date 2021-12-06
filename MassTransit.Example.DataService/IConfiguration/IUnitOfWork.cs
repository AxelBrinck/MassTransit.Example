using MassTransit.Example.DataService.IRepository;

namespace MassTransit.Example.DataService.IConfiguration
{
    public interface IUnitOfWork
    {
        IUsersRepository UserRepository { get; }
        Task CompleteAsync();
    }
}