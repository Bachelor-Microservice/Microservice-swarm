using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Dtos
{
    public class GroupsDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? CurrencyId { get; set; }
        public List<ItemDTO> Items { get; set; }
    }
}
