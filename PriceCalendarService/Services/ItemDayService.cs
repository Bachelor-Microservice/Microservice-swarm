using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PriceCalendarService.Dtos;
using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public async Task<ServiceResponse<ItemDayListDTO>> Add(ItemDayListDTO cmd)
        {
            var toCreate = new List<ItemDay>();
            var toUpdate = new List<ItemDay>();
            var existing = await _context.ItemDay.ToListAsync();
            
            foreach(var dto in cmd.ItemDays)
            {
                var model = _mapper.Map<ItemDay>(dto);
                if(existing.Exists(x => x.Id == model.Id))
                {
                    toUpdate.Add(model);
                }
                else
                {
                    toCreate.Add(model);
                }
            }

            await _context.ItemDay.AddRangeAsync(toCreate);
            _context.ItemDay.UpdateRange(toUpdate);
            await _context.SaveChangesAsync();
            return new ServiceResponse<ItemDayListDTO>{ Data = cmd };
        }

        public async Task<ServiceResponse<List<ItemDayDTO>>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
