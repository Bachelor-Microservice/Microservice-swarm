using System;
using System.Collections.Generic;
using System.Text;

namespace ContractsV2.BookingContracts
{
    public interface IBookedDay
    {
        double PriceForDay { get; set; }

        DateTime Date { get; set; }

        string DiscountDescription { get; set; }

        int ItemDayId { get; set; }
    }
}
