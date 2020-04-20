using System;
using System.Collections.Generic;

namespace PriceCalendarService.Models
{
    public class CustomerType
    {
        public Guid id { get; set; }

        public string Description { get; set; }

        public List<Group> Groups { get; set; }
    }
}