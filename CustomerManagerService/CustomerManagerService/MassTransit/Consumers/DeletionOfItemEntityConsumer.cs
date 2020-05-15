using AutoMapper;
using ContractsV2.ItemContracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerService.MassTransit.Consumers
{
    public class DeletionOfItemEntityConsumer : IConsumer<IItemEntityDeleted>
    {
        private readonly IMapper _mapper;

        public DeletionOfItemEntityConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<IItemEntityDeleted> context)
        {
            Console.WriteLine("Received deleted event from CUSTOMERMAN...");
            Console.WriteLine("Operation done...");
            return Task.CompletedTask;
        }
    }
}
