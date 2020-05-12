using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class Booking
    {
        public string Id { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Depature { get; set; }
        public double Price { get; set; }
        public string Customerid { get; set; }
        public string CustomerName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemName { get; set; }
        public string ItemNo { get; set; }
        public List<BookedDay> BookedDays { get; set; }
        public string Currency { get; set; }

    }
}
