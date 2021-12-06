using MassTransit.Example.DataService.IConfiguration;
using MassTransit.Example.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Example.Api.Controllers.V1
{
    public class UsersController : BaseController
    {
        public UsersController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpGet]
        [Route(nameof(GetUsersAsync))]
        public async Task<ActionResult> GetUsersAsync()
        {
            var users = await UnitOfWork.UserRepository.All();

            return Ok(users);
        }

        [HttpPost]
        [Route(nameof(AddUserAsync))]
        public async Task<IActionResult> AddUserAsync(string username)
        {
            var userEntity = new UserEntity();
            userEntity.Username = username;
            
            await UnitOfWork.UserRepository.Add(userEntity);
            await UnitOfWork.CompleteAsync();

            return CreatedAtRoute(nameof(GetUserAsync), userEntity.Id, userEntity);
        }
        
        [HttpGet]
        [Route(nameof(GetUserAsync))]
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var user = await UnitOfWork.UserRepository.GetById(id);

            return Ok(user);
        }
    }
}