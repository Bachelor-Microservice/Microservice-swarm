using AutoMapper;
using ContractsV2.ItemContracts;
using ItemContracts;
using MassTransit;
using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.MassTransit.Consumers
{
    public class DeletionOfItemEntityConsumer : IConsumer<IItemEntityDeleted>
    {
        private readonly PriceCalendarServiceContext _serviceContext;
        private readonly IMapper _mapper;

        public DeletionOfItemEntityConsumer(PriceCalendarServiceContext context, IMapper mapper)
        {
            _serviceContext = context;
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<IItemEntityDeleted> context)
        {
            Console.WriteLine("Received deleted event...");
            Delete(context);
            Console.WriteLine("Operation done...");
            return Task.CompletedTask;
        }

        public void Delete(ConsumeContext<IItemEntityDeleted> context)
        {
            var existing = _serviceContext.Item.FirstOrDefault(x => x.Id == context.Message.ItemNo);
            if (existing != null) _serviceContext.Item.Remove(existing);
        }
    }
}
