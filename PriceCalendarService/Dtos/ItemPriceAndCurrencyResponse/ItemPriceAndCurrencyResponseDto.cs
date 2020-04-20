using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Dtos.ItemPriceAndCurrencyResponse
{
    public class ItemPriceAndCurrencyResponseDto
    {
        public string Currency { get; set; }
        public List<Group> Groups { get; set; }
    }
}
