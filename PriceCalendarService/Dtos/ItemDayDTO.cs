using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Dtos
{
    public class ItemDayDTO
    {
        public DateTime? Date { get; set; }
        public int Id { get; set; }
        public double? Price { get; set; }
        public string Priority { get; set; }
        public string PricePackage { get; set; }
        public string CustomerId { get; set; }
        public string CustomerDescription { get; set; }
        public string ItemId { get; set; }
    }
}
