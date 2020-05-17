using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.DTO
{
    public class CreateBookingDTO
    {
        public DateTime Arrival { get; set; }
        public string Email { get; set; }

        public DateTime Depature { get; set; }

        public double Price { get; set; }

        public string Customerid { get; set; }

        public string CustomerName { get; set; }

        public string ItemDescription { get; set; }

        public string ItemName { get; set; }

        public string ItemNo { get; set; }

        public List<BookedDayDTO> BookedDays { get; set; }

        public string Currency { get; set; }
    }
}
