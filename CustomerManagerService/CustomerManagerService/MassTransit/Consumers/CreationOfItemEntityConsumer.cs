using AutoMapper;
using ContractsV2.ItemContracts;
using MassTransit;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerService.MassTransit.Consumers
{
    public class CreationOfItemEntityConsumer : IConsumer<IItemEntityCreated>
    {
        private readonly IMapper _mapper;

        public CreationOfItemEntityConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        //public CreationOfItemEntityConsumer() { }

        public Task Consume(ConsumeContext<IItemEntityCreated> consumedContext)
        {
            Console.WriteLine($"Received Create Event from CUSTOMERMAN...");
            Console.WriteLine("Name: " + consumedContext.Message.Name);
            Console.WriteLine("Event reaction done...");
            return Task.CompletedTask;
        }

    }
}