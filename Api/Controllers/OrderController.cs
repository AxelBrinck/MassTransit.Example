using System;
using System.Threading.Tasks;
using MassTransit;
using MessageContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IRequestClient<ISubmitOrder> _requestClient;

        public OrderController(
            ILogger<OrderController> logger,
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