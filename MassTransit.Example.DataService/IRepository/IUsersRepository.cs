using MassTransit.Example.Entities;

namespace MassTransit.Example.DataService.IRepository
{
    public interface IUsersRepository : IGenericRepository<UserEntity>
    {
        
    }
}