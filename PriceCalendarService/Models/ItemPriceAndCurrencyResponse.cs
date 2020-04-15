using System.Collections.Generic;

namespace PriceCalendarService.Models
{
    public class ItemPriceAndCurrencyResponse
    {
        public string Currency { get; set; }

        public List<Group> Groups { get; set; }
    }
}