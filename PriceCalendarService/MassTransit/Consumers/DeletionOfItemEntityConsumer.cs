using AutoMapper;
using ItemContracts;
using MassTransit;
using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.MassTransit.Consumers
{
    public class DeletionOfItemEntityConsumer : IConsumer<ItemContracts.ItemEntityDeleted>
    {
        private readonly PriceCalendarServiceContext _serviceContext;
        private readonly IMapper _mapper;

        public DeletionOfItemEntityConsumer(PriceCalendarServiceContext context, IMapper mapper)
        {
            _serviceContext = context;
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<ItemEntityDeleted> context)
        {
            Console.WriteLine("Received deleted event...");
            //_serviceContext.Item.FirstOrDefault(x => x.Id == context.Message.Id)
            return Task.CompletedTask;
        }
    }
}
