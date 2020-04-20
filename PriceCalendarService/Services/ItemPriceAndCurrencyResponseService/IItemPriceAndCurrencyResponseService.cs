using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Services.ItemPriceAndCurrencyResponseService
{
    public interface IItemPriceAndCurrencyResponseService
    {
        Task<ServiceResponse<List<ItemPriceAndCurrencyResponse>>> GetAll();
        Task<ServiceResponse<ItemPriceAndCurrencyResponse>> GetById(Guid id);
        Task<ServiceResponse<List<ItemPriceAndCurrencyResponse>>> Add(ItemPriceAndCurrencyResponse newItemPriceAndCurrencyResponse);

    }
}
