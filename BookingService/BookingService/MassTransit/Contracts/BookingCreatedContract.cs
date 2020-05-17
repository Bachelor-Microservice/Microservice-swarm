using ContractsV2.BookingContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.MassTransit.Contracts
{
    public class BookingCreatedContract : IBookingCreated
    {
        public string Id { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Depature { get; set; }
        public double Price { get; set; }
        public string Customerid { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string ItemDescription { get; set; }
        public string ItemName { get; set; }
        public string ItemNo { get; set; }
        public List<IBookedDay> BookedDays { get; set; }
        public string Currency { get; set; }
    }
}
