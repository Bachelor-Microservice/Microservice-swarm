using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.DTO
{
    public class BookedDayDTO
    {
        public double PriceForDay { get; set; }

        public DateTime Date { get; set; }

        public string DiscountDescription { get; set; }

        public int ItemDayId { get; set; }
    }
}
