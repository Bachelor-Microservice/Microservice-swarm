using AutoMapper;
using BookingService.MassTransit.Contracts;
using BookingService.Models;
using ContractsV2.BookingContracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.MassTransit.Publishers
{
    public interface IPublishBookingCrud
    {
        public void Deleted(string Id);
        public void Updated(Booking booking);

        public void Created(Booking booking);
    }
    public class PublishBookingCrud : IPublishBookingCrud
    {
        private readonly IMapper _mapper;
        private readonly IBus _bus;
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishBookingCrud(IMapper mapper, IBus bus, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _bus = bus;
            _publishEndpoint = publishEndpoint;
        }

        public async void Created(Booking booking)
        {
            var contract = _mapper.Map<BookingCreatedContract>(booking);
            contract.ItemNo = booking.Id;
            await _publishEndpoint.Publish<IBookingCreated>(contract);
        }

        public async void Deleted(string Id)
        {
            var contract = new BookingDeletedContract { Id = Id };
            await _publishEndpoint.Publish<IBookingDeleted>(contract);
        }

        public async void Updated(Booking booking)
        {
            var contract = _mapper.Map<BookingUpdatedContract>(booking);
            await _publishEndpoint.Publish<IBookingUpdated>(contract);
        }
    }
}
