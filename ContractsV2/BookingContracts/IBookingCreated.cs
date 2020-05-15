using System;
using System.Collections.Generic;
using System.Text;

namespace ContractsV2.BookingContracts
{
    public interface IBookingCreated
    {
        string Id { get; set; }

        DateTime Arrival { get; set; }

        DateTime Depature { get; set; }

        double Price { get; set; }

        string Customerid { get; set; }

        string CustomerName { get; set; }

        string Email { get; set; }

        string ItemDescription { get; set; }

        string ItemName { get; set; }

        string ItemNo { get; set; }

        List<IBookedDay> BookedDays { get; set; }

        string Currency { get; set; }
    }
}
