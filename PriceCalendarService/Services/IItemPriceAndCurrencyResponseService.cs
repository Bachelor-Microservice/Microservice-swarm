using PriceCalendarService.Dtos;
using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Services
{
    public interface IItemPriceAndCurrencyResponseService
    {
        public Task<ServiceResponse<List<ItemPriceAndCurrencyResponseDTO>>> GetAll();

        public Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Get(int id);

        public Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Add(ItemPriceAndCurrencyResponseDTO cmd);

        public Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Update(ItemPriceAndCurrencyResponseDTO cmd);

        public Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Delete(int Id);
    }
}
