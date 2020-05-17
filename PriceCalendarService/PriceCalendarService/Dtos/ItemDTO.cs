using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Dtos
{
    public class ItemDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int? GroupId { get; set; }
        public List<ItemDayDTO> ItemDays { get; set; }
    }
}
