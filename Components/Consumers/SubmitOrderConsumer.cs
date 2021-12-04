using System.Threading.Tasks;
using MassTransit;
using MessageContracts;

namespace Components.Consumers
{
    public class SubmitOrderConsumer : IConsumer<ISubmitOrder>
    {
        public async Task Consume(ConsumeContext<ISubmitOrder> context)
        {
            await context.RespondAsync<IOrderSubmissionAccepted>(new
            {
                context.Message.OrderId,
                InVar.Timestamp,
                context.Message.CustomerNumber
            });
        }
    }
}