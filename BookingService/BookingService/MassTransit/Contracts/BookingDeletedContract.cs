using ContractsV2.BookingContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.MassTransit.Contracts
{
    public class BookingDeletedContract : IBookingDeleted
    {
        public string Id { get; set; }
    }
}
