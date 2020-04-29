using AutoMapper;
using PriceCalendarService.Dtos;
using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Services
{
    public class ItemDayService : IItemDayService
    {
        private readonly PriceCalendarServiceContext _context;
        private readonly IMapper _mapper;
        public ItemDayService(PriceCalendarServiceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ItemDayDTO>> Add(ItemDayDTO cmd)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<ItemDayDTO>>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
