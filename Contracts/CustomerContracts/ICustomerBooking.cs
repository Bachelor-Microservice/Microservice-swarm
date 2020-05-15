using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.CustomerContracts
{
    public interface ICustomerBooking
    {
        DateTime Arrival { get; set; }

        DateTime Depature { get; set; }

        string ItemName { get; set; }

        string ItemNo { get; set; }

        string Price { get; set; }
    }
}
