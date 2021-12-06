using MassTransit.Example.DataService.IConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Example.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork UnitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}