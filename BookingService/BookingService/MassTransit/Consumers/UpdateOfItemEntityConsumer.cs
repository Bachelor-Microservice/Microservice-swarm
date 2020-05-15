using AutoMapper;
using ContractsV2.ItemContracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.MassTransit.Consumers
{
    public class UpdateOfItemEntityConsumer : IConsumer<IItemEntityUpdated>
    {
        private readonly IMapper _mapper;

        public UpdateOfItemEntityConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<IItemEntityUpdated> context)
        {
            Console.WriteLine("Received UpdateItemEntity Event from CUSTOMERMAN...");
            return Task.CompletedTask;
        }
    }
}
