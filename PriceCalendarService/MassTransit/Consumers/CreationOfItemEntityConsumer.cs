using ItemContracts;
using MassTransit;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.MassTransit.Consumers
{
    public class CreationOfItemEntityConsumer : IConsumer<ItemContracts.ItemEntityCreated>
    {
        public Task Consume(ConsumeContext<ItemEntityCreated> context)
        {
            Console.WriteLine($"CREATE: Receive message value: {context.Message.Id}");
            
            return Task.CompletedTask;
        }
    }
}
