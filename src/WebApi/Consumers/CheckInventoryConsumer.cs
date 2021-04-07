namespace WebApi.Consumers
{
    using System;
    using System.Threading.Tasks;
    using Contracts;
    using MassTransit;


    public class CheckInventoryConsumer :
        IConsumer<CheckInventory>
    {
        readonly Token _token;

        public CheckInventoryConsumer(Token token)
        {
            _token = token;
        }

        public Task Consume(ConsumeContext<CheckInventory> context)
        {
            if (string.IsNullOrWhiteSpace(_token.Value))
                throw new InvalidOperationException("The security token was not found");

            return context.RespondAsync(new InventoryStatus
            {
                Sku = context.Message.Sku,
                QuantityOnHand = new Random().Next(100)
            });
        }
    }
}