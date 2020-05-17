using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using PriceCalendarService.Dtos;
using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using PriceCalendarService.Hubs;
using Groups = PriceCalendarService.Models.Groups;
using Item = PriceCalendarService.Models.Item;
using Serilog;

namespace PriceCalendarService.Services
{
    public class ItemPriceAndCurrencyResponseService : IItemPriceAndCurrencyResponseService
    {
        private readonly PriceCalendarServiceContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ILogger _logger;

        public ItemPriceAndCurrencyResponseService(PriceCalendarServiceContext context, IMapper mapper
            , IHubContext<ChatHub> hubContext, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _hubContext = hubContext;
            _logger = logger;
        }
        public async Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Add(ItemPriceAndCurrencyResponseDTO dto)
        {
            var serviceResponse = new ServiceResponse<ItemPriceAndCurrencyResponseDTO>();
            var cmd = this.MapManuallyFromDtoToModel(dto);
            await _context.ItemPriceAndCurrencyResponse.AddAsync(cmd);
            await _context.SaveChangesAsync();
            serviceResponse.Data = dto;
            _logger.Information("PriceService - added itempriceandcurrencyresponse with currency: {Currency}", dto.Currency);
            return serviceResponse;
        }

        public async Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Delete(int Id)
        {
            var serviceResponse = new ServiceResponse<ItemPriceAndCurrencyResponseDTO>();
            var toBeDeleted = await _context.ItemPriceAndCurrencyResponse.FirstAsync(c => c.Id == Id);
            _context.ItemPriceAndCurrencyResponse.Remove(toBeDeleted);
            await _context.SaveChangesAsync();
            _logger.Information("PriceService - deleted itempriceandcurrencyresponse with currency: {Currency} and Id: {Id]"
                , toBeDeleted.Currency, toBeDeleted.Id); 
           serviceResponse.Data = this.MapManuallyFromModelToDto(toBeDeleted);
            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> ExportToExcel(DateTime @from, DateTime to)
        {
            BackgroundJob.Schedule( () =>  LongFormattingMethod(from , to), TimeSpan.FromSeconds(0));
            var serviceResponse = new ServiceResponse<string>();
            serviceResponse.Success = true;
            return serviceResponse;
        }
        
        public async Task LongFormattingMethod(DateTime @from, DateTime to)
        {
            System.Console.WriteLine("DATES");
            System.Console.WriteLine(from.ToString() , to);
            System.IO.Stream spreadsheetStream = new System.IO.MemoryStream();
            var wb = new XLWorkbook();
            IXLWorksheet worksheet = wb.Worksheets.Add("test");

            var ItemPriceAndCurrencyResponseList = await _context.ItemPriceAndCurrencyResponse
                .Include(i => i.Groups)
                .ThenInclude(g => g.Item)
                .ThenInclude(o => o.ItemDay)
                .ToListAsync();
            
            var items = new List<Item>();
            worksheet.Cell(1, 1).SetValue("Id");
            worksheet.Cell(1, 2).SetValue("Navn");
            worksheet.Cell(1, 3).SetValue("Pris");
            int cellY = 2;
            int cellX = 1;
            foreach (var itemPriceAndCurrency in ItemPriceAndCurrencyResponseList)
            {
                foreach (var group in itemPriceAndCurrency.Groups)
                {
                    foreach (var item in group.Item)
                    {

                        worksheet.Cell(cellY, cellX++).SetValue(item.Id);
                        worksheet.Cell(cellY, cellX++).SetValue(item.Name);
                        worksheet.Cell(cellY, cellX++).SetValue(item.Price);
                        items.Add(item);
                        cellY++;
                        cellX = 1;

                    }
                }
            }



            cellX = 3;
            var isItemPrice = true;
            for (var day = from.Date; day <= to; day = day.AddDays(1))
            {
                cellX++;
                cellY = 1;
                
                worksheet.Cell(cellY++, cellX).SetValue(day.ToString("d"));
                foreach (var item in items)
                {
                    foreach (var itemDay in item.ItemDay)
                    {
                        isItemPrice = true;
                        if (itemDay.Date.Value == day.Date && item.Id == itemDay.ItemId)
                        {
                            worksheet.Cell(cellY, cellX).SetValue(itemDay.Price);
                            worksheet.Cell(cellY++, cellX).Style.Font.Bold = true; 
                            isItemPrice = false;
                            break;

                        } 
                    }

                    if (isItemPrice)
                    {
                        worksheet.Cell(cellY++, cellX).SetValue(item.Price);
                       
                    }
                    
                }
            }
            
        


            worksheet.ColumnWidth = 20;
            wb.SaveAs(spreadsheetStream);
            spreadsheetStream.Position = 0;
            var workbookBytes = new byte[0];
            await using (var ms = new MemoryStream())
            {
                wb.SaveAs(ms);
                workbookBytes = ms.ToArray();
            }

            System.Console.WriteLine(_hubContext);
            await _hubContext.Clients.All.SendAsync("Excel", workbookBytes);
        }

        public async Task<ServiceResponse<List<ItemPriceAndCurrencyResponseDTO>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<ItemPriceAndCurrencyResponseDTO>>();
            var model = await _context.ItemPriceAndCurrencyResponse
                .Include(i => i.Groups)
                .ThenInclude(g => g.Item)
                .ThenInclude(o => o.ItemDay)
                .ToListAsync();
            serviceResponse.Data = new List<ItemPriceAndCurrencyResponseDTO>();
            foreach (var item in model) serviceResponse.Data.Add(this.MapManuallyFromModelToDto(item));
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ItemPriceAndCurrencyResponseDTO>>> GetAllWithoutItems()
        {
            var serviceResponse = new ServiceResponse<List<ItemPriceAndCurrencyResponseDTO>>();
            var model = await _context.ItemPriceAndCurrencyResponse
                .Include(i => i.Groups)
                .ToListAsync();
            serviceResponse.Data = new List<ItemPriceAndCurrencyResponseDTO>();
            foreach (var item in model) serviceResponse.Data.Add(this.MapManuallyFromModelToDto(item));
            return serviceResponse;
        }

        public async Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Get(int id)
        {
            var serviceResponse = new ServiceResponse<ItemPriceAndCurrencyResponseDTO>();
            var model = await _context.ItemPriceAndCurrencyResponse
                .Include(o => o.Groups)
                .ThenInclude(g => g.Item)
                .ThenInclude(i => i.ItemDay)
                .FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = this.MapManuallyFromModelToDto(model);
            _logger.Information("PriceService - returned itempriceandcurrencyresponse with currency: {Currency} and Id: {Id]"
                , model.Currency, model.Id);
            return serviceResponse;
        }

        public async Task<ServiceResponse<ItemPriceAndCurrencyResponseDTO>> Update(ItemPriceAndCurrencyResponseDTO dto)
        {
            var serviceResponse = new ServiceResponse<ItemPriceAndCurrencyResponseDTO>();
            var cmd = this.MapManuallyFromDtoToModel(dto);
            _context.ItemPriceAndCurrencyResponse.Update(cmd);
            await _context.SaveChangesAsync();
            serviceResponse.Data = dto;
            _logger.Information("PriceService - updated itempriceandcurrencyresponse with currency: {Currency} and Id: {Id]"
                , cmd.Currency, cmd.Id);
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
                        mItem.ItemDay.Add(mItemDay);
                    }
                }
            }
            return model;
        }

        private ItemPriceAndCurrencyResponseDTO MapManuallyFromModelToDto(ItemPriceAndCurrencyResponse model)
        {
            var dto = _mapper.Map<ItemPriceAndCurrencyResponseDTO>(model);
            if(model.Groups!= null) dto.Groups = new List<GroupsDTO>();
            foreach (var group in model.Groups)
            {
                var groupDTO = _mapper.Map<GroupsDTO>(group);
                if(group.Item != null) groupDTO.Items = new List<ItemDTO>();
                dto.Groups.Add(groupDTO);
                foreach (var item in group.Item)
                {
                    var itemDTO = _mapper.Map<ItemDTO>(item);
                    if (item.ItemDay != null) itemDTO.ItemDays = new List<ItemDayDTO>();
                    groupDTO.Items.Add(itemDTO);
                    foreach (var itemDay in item.ItemDay)
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
