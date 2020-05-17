using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.CustomerContracts
{
    public interface ICustomerUpdated
    {
        string Id { get; set; }

        string SupplementName { get; set; }

        string Type { get; set; }

        DateTime RegistrationDate { get; set; }

        string Email { get; set; }

        string Address { get; set; }

        string TelephonePrimary { get; set; }

        string MobilePhone { get; set; }
        List<ICustomerBooking> Bookings { get; set; }
    }
}
