using MassTransit.Example.MessageContracts;

namespace MassTransit.Example.Components.Consumers
{
    public class SubmitOrderConsumer : IConsumer<SubmitOrder>
    {
        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            throw new NotImplementedException();
        }
    }
}