using PriceCalendarService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Services
{
    public interface IItemDayService
    {
        public Task<ServiceResponse<List<ItemDayDTO>>> GetAll();

        public Task<ServiceResponse<ItemDayListDTO>> Add(ItemDayListDTO cmd);
    }
}
