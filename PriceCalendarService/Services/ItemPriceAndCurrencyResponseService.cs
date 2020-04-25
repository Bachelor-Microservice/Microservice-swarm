using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
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
            var cmd = this.MapManuallyFromDtoToModel(dto);
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
            serviceResponse.Data = this.MapManuallyFromModelToDto(toBeDeleted);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ItemPriceAndCurrencyResponseDTO>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<ItemPriceAndCurrencyResponseDTO>>();
            var model = await _context.ItemPriceAndCurrencyResponse.ToListAsync();
            serviceResponse.Data = new List<ItemPriceAndCurrencyResponseDTO>();
            foreach (var item in model) serviceResponse.Data.Add(this.MapManuallyFromModelToDto(item));
            return serviceResponse;
        }

        public async Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Get(int id)
        {
            var serviceResponse = new ServiceResponse<ItemPriceAndCurrencyResponseDTO>();
            var model = await _context.ItemPriceAndCurrencyResponse.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = this.MapManuallyFromModelToDto(model);
            return serviceResponse;
        }

        public async Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Update(ItemPriceAndCurrencyResponseDTO dto)
        {
            var serviceResponse = new ServiceResponse<ItemPriceAndCurrencyResponseDTO>();
            var cmd = this.MapManuallyFromDtoToModel(dto);
            _context.ItemPriceAndCurrencyResponse.Update(cmd);
            await _context.SaveChangesAsync();
            serviceResponse.Data = dto;
            return serviceResponse;
        }

        private ItemPriceAndCurrencyResponse MapManuallyFromDtoToModel(ItemPriceAndCurrencyResponseDTO dto)
        {
            var model = _mapper.Map<ItemPriceAndCurrencyResponse>(dto);
            model.Groups = new List<Groups>();
            foreach (var group in dto.Groups)
            {
                var mGroup = _mapper.Map<Groups>(group);
                mGroup.Currency = model;
                mGroup.CurrencyId = model.Id;
                model.Groups.Add(mGroup);
                foreach (var item in group.Items)
                {
                    var mItem = _mapper.Map<Item>(item);
                    mItem.Group = mGroup;
                    mItem.GroupId = mGroup.Id;
                    mGroup.Item.Add(mItem);
                    foreach (var itemDay in item.ItemDays)
                    {
                        var mItemDay = _mapper.Map<ItemDay>(itemDay);
                        mItemDay.Item = mItem;
                        mItemDay.ItemId = mItem.Id;
                    }
                }
            }
            return model;
        }

        private ItemPriceAndCurrencyResponseDTO MapManuallyFromModelToDto(ItemPriceAndCurrencyResponse model)
        {
            var dto = _mapper.Map<ItemPriceAndCurrencyResponseDTO>(model);
            foreach (var group in model.Groups)
            {
                var groupDTO = _mapper.Map<GroupsDTO>(group);
                dto.Groups.Add(groupDTO);
                foreach (var item in group.Item)
                {
                    var itemDTO = _mapper.Map<ItemDTO>(item);
                    groupDTO.Items.Add(itemDTO);
                    foreach (var itemDay in itemDTO.ItemDays)
                    {
                        var itemDayDTO = _mapper.Map<ItemDayDTO>(itemDay);
                        itemDTO.ItemDays.Add(itemDayDTO);
                    }
                }
            }
            return dto;
        }
    }
}
