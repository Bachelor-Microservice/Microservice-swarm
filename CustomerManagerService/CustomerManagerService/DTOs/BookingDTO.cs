using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerService.DTOs
{
    public class BookingDTO
    {
        
        
        public DateTime Arrival { get; set; }
        
        public DateTime Depature { get; set; }

        public string ItemName { get; set; }

        public string ItemNo { get; set; }
    }
}
