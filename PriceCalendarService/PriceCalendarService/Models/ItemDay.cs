using System;
using System.Collections.Generic;

namespace PriceCalendarService.Models
{
    public partial class ItemDay
    {
        public DateTime? Date { get; set; }
        public int Id { get; set; }
        public double? Price { get; set; }
        public string Priority { get; set; }
        public string PricePackage { get; set; }
        public string CustomerId { get; set; }
        public string CustomerDescription { get; set; }
        public string ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
