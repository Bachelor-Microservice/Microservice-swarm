using AutoMapper;
using ContractsV2.ItemContracts;
using ItemContracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceCalendarService.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.MassTransit.Consumers
{
    public class UpdateOfItemEntityConsumer : IConsumer<IItemEntityUpdated>
    {
        private readonly PriceCalendarServiceContext _serviceContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateOfItemEntityConsumer(PriceCalendarServiceContext context, IMapper mapper, ILogger logger)
        {
            _serviceContext = context;
            _mapper = mapper;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IItemEntityUpdated> context)
        {
            Console.WriteLine("PriceService - Received UpdateItemEntity Event...");
            _logger.Information("PriceService - Received UpdateItemEntity Event with Id: {ItemNo} ", context.Message.ItemNo);
            UpdateFromContext(context);
            return Task.CompletedTask;
        }

        public bool CheckForSuitability(ConsumeContext<IItemEntityUpdated> context)
        {
            var message = context.Message;
            if(string.IsNullOrWhiteSpace(message.ItemNo) || 
                message.ArticleGroup == null || message.ArticleGroup == default(int) ||
                message.RelationNo == null || message.RelationNo == default(int))
            {
                return false;
            }
            return true;
        }

        private void UpdateFromContext(ConsumeContext<IItemEntityUpdated> consumedContext)
        {
            Console.WriteLine("Checking for suitability...");
            if (!CheckForSuitability(consumedContext)) return; //Indæst Throw her
            Console.WriteLine("Message is suited...");
            var model = _serviceContext.ItemPriceAndCurrencyResponse
                .Include(o => o.Groups)
                .ThenInclude(g => g.Item)
                .FirstOrDefault(c => c.Id == consumedContext.Message.RelationNo);
            if(model != null)
            {
                Console.WriteLine("Existing model found matching...");
                MapFromContext(consumedContext, model);
                _serviceContext.ItemPriceAndCurrencyResponse.Update(model);
                _serviceContext.SaveChanges();
            }
            else
            {
                Console.WriteLine("No existing found");
                var group = _serviceContext.Groups
                    .Include(o => o.Item)
                    .FirstOrDefault(c => c.Id == consumedContext.Message.ArticleGroup);
                if(group != null)
                {
                    Console.WriteLine("Group found (no model above)...");
                    MapFromContext(consumedContext, group);
                    _serviceContext.Groups.Update(group);
                    _serviceContext.SaveChanges();
                }
                else
                {
                    Console.WriteLine("No group or model found --> attempting to match item");
                    var item = _serviceContext.Item.FirstOrDefault(i => i.Id == consumedContext.Message.ItemNo);
                    if (item != null) Console.WriteLine("Item Found...");
                    MapFromContext(consumedContext, item);
                    _serviceContext.Item.Update(item);
                    _serviceContext.SaveChanges();
                }
            }
        }

        public void MapFromContext(ConsumeContext<IItemEntityUpdated> from, ItemPriceAndCurrencyResponse to)
        {
            to.Currency = from.Message.Unit;
            foreach(var group in to.Groups)
            {
                if(group.Id == from.Message.ArticleGroup)
                {
                    MapFromContext(from, group);
                }
            } 
        }

        public void MapFromContext(ConsumeContext<IItemEntityUpdated> from, Groups to)
        {
            to.Description = from.Message.PriceModel;
            foreach(var item in to.Item)
            {
                if (item.Id == from.Message.ItemNo) MapFromContext(from, item);
            }
        }

        public void MapFromContext(ConsumeContext<IItemEntityUpdated> from, Item to)
        {
            to.Name = from.Message.Name;
            to.Price = from.Message.Price;
        }
    }
}
