using ItemContracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.MassTransit.Consumers
{
    public class DeletionOfItemEntityConsumers : IConsumer<ItemContracts.ItemEntityDeleted>
    {
        public Task Consume(ConsumeContext<ItemEntityDeleted> context)
        {
            Console.WriteLine($"DELETE: Receive message value: {context.Message.Id}");
            return Task.CompletedTask;
        }
    }
}
