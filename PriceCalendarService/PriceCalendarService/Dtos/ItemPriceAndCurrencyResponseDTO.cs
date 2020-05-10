using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Dtos
{
    public class ItemPriceAndCurrencyResponseDTO
    {
        public string Currency { get; set; }
        public int Id { get; set; }
        public List<GroupsDTO> Groups { get; set; }
    }
}
