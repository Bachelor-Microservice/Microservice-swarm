using System;
using System.Collections.Generic;

namespace PriceCalendarService.Models
{
    public partial class ItemPriceAndCurrencyResponse
    {
        public ItemPriceAndCurrencyResponse()
        {
            Groups = new HashSet<Groups>();
        }

        public int Id { get; set; }
        public string Currency { get; set; }

        public virtual ICollection<Groups> Groups { get; set; }
    }
}
