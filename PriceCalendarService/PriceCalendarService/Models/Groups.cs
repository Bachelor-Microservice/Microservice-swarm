using System;
using System.Collections.Generic;

namespace PriceCalendarService.Models
{
    public partial class Groups
    {
        public Groups()
        {
            Item = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int? CurrencyId { get; set; }

        public virtual ItemPriceAndCurrencyResponse Currency { get; set; }
        public virtual ICollection<Item> Item { get; set; }
    }
}
