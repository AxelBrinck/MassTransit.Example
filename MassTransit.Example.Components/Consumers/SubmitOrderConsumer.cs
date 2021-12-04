using MassTransit;
using MessageContracts;

namespace MassTransit.Example.Components.Consumers
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