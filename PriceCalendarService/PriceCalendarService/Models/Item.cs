using System;
using System.Collections.Generic;

namespace PriceCalendarService.Models
{
    public partial class Item
    {
        public Item()
        {
            ItemDay = new HashSet<ItemDay>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int? GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual ICollection<ItemDay> ItemDay { get; set; }
    }
}
