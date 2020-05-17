using AutoMapper;
using ContractsV2.ItemContracts;
using ItemContracts;
using MassTransit;
using PriceCalendarService.Models;
using Serilog;
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
        private readonly ILogger _logger;

        public DeletionOfItemEntityConsumer(PriceCalendarServiceContext context, IMapper mapper, ILogger logger)
        {
            _serviceContext = context;
            _mapper = mapper;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IItemEntityDeleted> context)
        {
            Console.WriteLine("Received deleted event...");
            _logger.Information("PriceService - Received IItemEntityDeleted event with context Id: {ItemNo}", context.Message.ItemNo);
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
