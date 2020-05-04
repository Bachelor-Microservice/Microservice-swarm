using ItemContracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.MassTransit.Consumers
{
    public class UpdateOfItemEntityConsumer : IConsumer<ItemContracts.ItemEntityUpdated>
    {
        public Task Consume(ConsumeContext<ItemEntityUpdated> context)
        {
            Console.WriteLine($"UPDATE: Receive message value: {context.Message.Id}");
            return Task.CompletedTask;
        }
    }
}
