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
        Task<ServiceResponse<List<ItemPriceAndCurrencyResponseDTO>>> GetAll();

        Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Get(int id);

        Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Add(ItemPriceAndCurrencyResponseDTO cmd);

        Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Update(ItemPriceAndCurrencyResponseDTO cmd);

        Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Delete(int Id);
        
        Task<ServiceResponse<string>> ExportToExcel(DateTime from, DateTime to);

        Task<ServiceResponse<List<ItemPriceAndCurrencyResponseDTO>>> GetAllWithoutItems();
    }
}
