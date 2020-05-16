using AutoMapper;
using ContractsV2.BookingContracts;
using ContractsV2.ItemContracts;
using CustomerManagerService.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerService.MassTransit.Consumers
{
    public class DeletionOfBookingConsumer : IConsumer<IBookingDeleted>
    {
        private readonly IMapper _mapper;
        private readonly CustomerService _service;

        public DeletionOfBookingConsumer(IMapper mapper , CustomerService service)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task Consume(ConsumeContext<IBookingDeleted> context)
        {
            Console.WriteLine("Received deleted event...");
            var existing = _service.FindFromItemNo(context.Message.Id);
            if (existing != null) Console.WriteLine("Existing Found...");
            foreach (var booking in existing.Bookings.ToList())
                if (booking.ItemNo == context.Message.Id)
                    existing.Bookings.Remove(booking);

           await _service.Update(existing.Id, existing);

            Console.WriteLine("Operation done...");
        }
    }
}
