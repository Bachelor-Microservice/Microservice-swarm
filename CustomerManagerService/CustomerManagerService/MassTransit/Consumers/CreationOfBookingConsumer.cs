using AutoMapper;
using ContractsV2.BookingContracts;
using ContractsV2.ItemContracts;
using CustomerManagerService.Models;
using CustomerManagerService.Services;
using MassTransit;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerService.MassTransit.Consumers
{
    public class CreationOfBookingConsumer : IConsumer<IBookingCreated>
    {
        private readonly IMapper _mapper;
        private readonly CustomerService _service;

        public CreationOfBookingConsumer(IMapper mapper, CustomerService service)
        {
            _mapper = mapper;
            _service = service;
        }

        //public CreationOfItemEntityConsumer() { }

        public async Task Consume(ConsumeContext<IBookingCreated> consumedContext)
        {
            Console.WriteLine($"Received Create Event...");

            var contract = consumedContext.Message;
            var existing = await _service.Get(contract.Customerid);
            
            if(existing != null)
            {
                existing.Bookings.Add(MappingHelper.MapFromExisting(contract));
                await _service.Update(existing.Id, existing);
            }
            else
            {
                await _service.Create(MappingHelper.Map(contract));
            }
            Console.WriteLine("Event reaction done...");
        }

    }
}