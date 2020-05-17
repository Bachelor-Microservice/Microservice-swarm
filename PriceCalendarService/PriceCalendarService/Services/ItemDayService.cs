using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using PriceCalendarService.Dtos;
using PriceCalendarService.Models;
using Serilog;
using Serilog.Core;
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
        private readonly ILogger _logger;
        public ItemDayService(PriceCalendarServiceContext context, IMapper mapper, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<ItemDayListDTO>> Add(ItemDayListDTO cmd)
        {
            var toCreate = new List<ItemDay>();
            var toUpdate = new List<ItemDay>();
            var existing = await _context.ItemDay.ToListAsync();
            
            foreach(var dto in cmd.ItemDays)
            {
                var existingItem = existing.Find(x => x.Id == dto.Id);
                if(existingItem != null)
                {
                    if(dto.Price != 0) existingItem.Price = dto.Price;
                    if (!String.IsNullOrWhiteSpace(dto.PricePackage)) existingItem.PricePackage = dto.PricePackage;
                    if(!String.IsNullOrWhiteSpace(dto.Priority)) existingItem.Priority = dto.Priority;
                    if(dto.Date != null) existingItem.Date = dto.Date;
                    if(!String.IsNullOrWhiteSpace(dto.CustomerDescription)) existingItem.CustomerDescription = dto.CustomerDescription;
                    if (!String.IsNullOrWhiteSpace(dto.ItemId)) existingItem.ItemId = dto.ItemId;
                    toUpdate.Add(existingItem);
                }
                else
                {
                    var model = _mapper.Map<ItemDay>(dto);
                    Random rnd = new Random();
                    model.Id = rnd.Next(1000000, 999999999);

                    toCreate.Add(model);
                }
                
            }

            if(toCreate.Count != 0) await _context.ItemDay.AddRangeAsync(toCreate);
            if(toUpdate.Count != 0) _context.ItemDay.UpdateRange(toUpdate);
            await _context.SaveChangesAsync();
            _logger.Information("PriceService: Added itemday list");
            return new ServiceResponse<ItemDayListDTO>{ Data = cmd };
        }

        public async Task<ServiceResponse<List<ItemDayDTO>>> GetAll()
        {
            var itemDays = await _context.ItemDay.ToListAsync();
            var dtos = new List<ItemDayDTO>();
            foreach (var item in itemDays) dtos.Add(_mapper.Map<ItemDayDTO>(item));
            return new ServiceResponse<List<ItemDayDTO>> { Data = dtos};
        }

        public Task<ServiceResponse<ItemDayDTO>> Add(ItemDayDTO cmd)
        {
            throw new NotImplementedException();
        }
    }
}
