using MassTransit.Example.DataService.IConfiguration;
using MessageContracts;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Example.Api.Controllers.V1
{
    public class OrdersController : BaseController
    {
        private readonly IRequestClient<ISubmitOrder> _requestClient;

        public OrdersController(
            IRequestClient<ISubmitOrder> requestClient,
            IUnitOfWork unitOfWork) :
                base(unitOfWork)
        {
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