using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PriceCalendarService.Dtos;
using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Services
{
    public class ItemPriceAndCurrencyResponseService : IItemPriceAndCurrencyResponseService
    {
        private readonly PriceCalendarServiceContext _context;
        private readonly IMapper _mapper;

        public ItemPriceAndCurrencyResponseService(PriceCalendarServiceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Add(ItemPriceAndCurrencyResponseDTO dto)
        {
            var serviceResponse = new ServiceResponse<ItemPriceAndCurrencyResponseDTO>();
            var cmd = _mapper.Map<ItemPriceAndCurrencyResponse>(dto);
            SetTableRelationships(cmd);
            await _context.ItemPriceAndCurrencyResponse.AddAsync(cmd);
            await _context.SaveChangesAsync();
            serviceResponse.Data = dto;
            return serviceResponse;
        }

        public async Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Delete(int Id)
        {
            var serviceResponse = new ServiceResponse<ItemPriceAndCurrencyResponseDTO>();
            var toBeDeleted = await _context.ItemPriceAndCurrencyResponse.FirstAsync(c => c.Id == Id);
            _context.ItemPriceAndCurrencyResponse.Remove(toBeDeleted);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _mapper.Map<ItemPriceAndCurrencyResponseDTO>(toBeDeleted);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ItemPriceAndCurrencyResponseDTO>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<ItemPriceAndCurrencyResponseDTO>>();
            var model = await _context.ItemPriceAndCurrencyResponse.ToListAsync();
            serviceResponse.Data = _mapper.Map<List<ItemPriceAndCurrencyResponseDTO>>(model);
            return serviceResponse;
        }

        public async Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Get(int id)
        {
            var serviceResponse = new ServiceResponse<ItemPriceAndCurrencyResponseDTO>();
            var model = await _context.ItemPriceAndCurrencyResponse.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<ItemPriceAndCurrencyResponseDTO>(model);
            return serviceResponse;
        }

        public async Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Update(ItemPriceAndCurrencyResponseDTO dto)
        {
            var serviceResponse = new ServiceResponse<ItemPriceAndCurrencyResponseDTO>();
            var cmd = _mapper.Map<ItemPriceAndCurrencyResponse>(dto);
            SetTableRelationships(cmd);
            _context.ItemPriceAndCurrencyResponse.Update(cmd);
            await _context.SaveChangesAsync();
            serviceResponse.Data = dto;
            return serviceResponse;
        }

        private async void SetTableRelationships(ItemPriceAndCurrencyResponse model)
        {
            foreach (var group in model.Groups)
            {
                group.Currency = model;
                foreach(var item in group.Item)
                {
                    item.Group = group;
                    foreach (var day in item.ItemDay) day.Item = item;
                }
            }
        }
    }
}
