using PriceCalendarService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Services
{
    public interface IItemDayService
    {
        Task<ServiceResponse<List<ItemDayDTO>>> GetAll();

        Task<ServiceResponse<ItemDayDTO>> Add(ItemDayDTO cmd);
    }
}
