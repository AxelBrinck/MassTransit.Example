using MassTransit;
using MessageContracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IRequestClient<ISubmitOrder> _requestClient;

        public OrdersController(
            ILogger<OrdersController> logger,
            IRequestClient<ISubmitOrder> requestClient)
        {
            _logger = logger;
            _requestClient = requestClient;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Guid id, string customerNumber)
        {
            var response = await _requestClient.GetResponse<IOrderSubmissionAccepted>(new
            {
                OrderId = id,
                InVar.Timestamp,
                CustomerNumber = customerNumber
            });

            return Ok(response.Message);
        }
    }
}