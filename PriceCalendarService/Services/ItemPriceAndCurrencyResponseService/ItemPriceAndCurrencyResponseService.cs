using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Services.ItemPriceAndCurrencyResponseService
{
    public class ItemPriceAndCurrencyResponseService : IItemPriceAndCurrencyResponseService
    {
        public async Task<ServiceResponse<List<ItemPriceAndCurrencyResponse>>> Add(ItemPriceAndCurrencyResponse newItemPriceAndCurrencyResponse)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<ItemPriceAndCurrencyResponse>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<ItemPriceAndCurrencyResponse>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
