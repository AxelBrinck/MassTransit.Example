using MassTransit.Example.DataService.Data;
using MassTransit.Example.Entities;
using MassTransit.Example.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            AppDbContext context,
            ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> GetUsers()
        {
            var users = _context.Users.ToList();

            return Ok(users);
        }

        [HttpPost]
        public IActionResult AddUser(string username)
        {
            var userEntity = new UserEntity();
            userEntity.Username = username;
            
            _context.Users.Add(userEntity);

            _context.SaveChangesAsync();

            return Ok();
        }
    }
}