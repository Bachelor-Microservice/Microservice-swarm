using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerService.DTOs
{
    public class UpdateCustomerDTO
    {
        public string Id { get; set; }
        public string SupplementName { get; set; }

        public string Type { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string TelephonePrimary { get; set; }

        public string MobilePhone { get; set; }

        public List<BookingDTO> Bookings { get; set; }
    }
}
