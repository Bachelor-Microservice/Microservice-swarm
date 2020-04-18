using System;

namespace PriceCalendarService.Models
{
    public class GetItemPriceAndCurrencyCommand
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}