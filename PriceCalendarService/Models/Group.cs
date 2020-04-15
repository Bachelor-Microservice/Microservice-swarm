using System.Collections.Generic;

namespace PriceCalendarService.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public List<Item> Items { get; set; }
    }
}