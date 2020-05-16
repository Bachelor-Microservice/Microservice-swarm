using AutoMapper;
using ContractsV2.BookingContracts;
using ContractsV2.ItemContracts;
using CustomerManagerService.Models;
using CustomerManagerService.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerService.MassTransit.Consumers
{
    public class UpdateOfBookingConsumer : IConsumer<IBookingUpdated>
    {
        private readonly IMapper _mapper;
        private readonly CustomerService _service;

        public UpdateOfBookingConsumer(IMapper mapper, CustomerService service)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task Consume(ConsumeContext<IBookingUpdated> context)
        {
            Console.WriteLine("Received UpdateBooking Event...");
            var existing = await _service.Get(context.Message.Customerid);
            
            if(existing != null)
            {
                if (existing.Bookings != null)
                {
                    foreach(var booking in existing.Bookings)
                    {
                        if (booking.ItemNo == context.Message.Id)
                        {
                            var phy = context.Message;
                            if (!string.IsNullOrWhiteSpace(phy.CustomerName)) existing.SupplementName = phy.CustomerName;
                            if (!string.IsNullOrWhiteSpace(phy.Email)) existing.SupplementName = phy.Email;
                            if (phy.Arrival != null) booking.Arrival = phy.Arrival;
                            if (phy.Depature != null) booking.Depature = phy.Depature;
                            if (phy.Price != default && !string.IsNullOrWhiteSpace(phy.Currency)) booking.Price = phy.Currency + ": " + phy.Price.ToString();
                            await _service.Update(existing.Id, existing);
                        }
                    }
                }
            }
        }
    }
}
