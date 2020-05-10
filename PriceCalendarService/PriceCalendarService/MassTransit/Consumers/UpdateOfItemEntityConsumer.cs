﻿using AutoMapper;
using ItemContracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceCalendarService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.MassTransit.Consumers
{
    public class UpdateOfItemEntityConsumer : IConsumer<ItemContracts.ItemEntityUpdated>
    {
        private readonly PriceCalendarServiceContext _serviceContext;
        private readonly IMapper _mapper;

        public UpdateOfItemEntityConsumer(PriceCalendarServiceContext context, IMapper mapper)
        {
            _serviceContext = context;
            _mapper = mapper;
        }

        public Task Consume(ConsumeContext<ItemEntityUpdated> context)
        {
            Console.WriteLine("Received UpdateItemEntity Event...");
            UpdateFromContext(context);
            return Task.CompletedTask;
        }

        public bool CheckForSuitability(ConsumeContext<ItemEntityUpdated> context)
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

        private async void UpdateFromContext(ConsumeContext<ItemEntityUpdated> consumedContext)
        {
            if (CheckForSuitability(consumedContext) == false) return;
            var model = await _serviceContext.ItemPriceAndCurrencyResponse
                .Include(o => o.Groups)
                .ThenInclude(g => g.Item)
                .FirstOrDefaultAsync(c => c.Id == consumedContext.Message.RelationNo);
            if(model != null)
            {
                MapFromContext(consumedContext, model);
                _serviceContext.ItemPriceAndCurrencyResponse.Update(model);
                await _serviceContext.SaveChangesAsync();
            }
            else
            {
                var group = await _serviceContext.Groups
                    .Include(o => o.Item)
                    .FirstOrDefaultAsync(c => c.Id == consumedContext.Message.ArticleGroup);
                if(group != null)
                {
                    MapFromContext(consumedContext, group);
                    _serviceContext.Groups.Update(group);
                    await _serviceContext.SaveChangesAsync();
                }
                else
                {
                    var item = await _serviceContext.Item.FirstOrDefaultAsync(i => i.Id == consumedContext.Message.ItemNo);
                    MapFromContext(consumedContext, item);
                    _serviceContext.Item.Update(item);
                    await _serviceContext.SaveChangesAsync();
                }
            }
        }

        public void MapFromContext(ConsumeContext<ItemEntityUpdated> from, ItemPriceAndCurrencyResponse to)
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

        public void MapFromContext(ConsumeContext<ItemEntityUpdated> from, Groups to)
        {
            //No relevant information for groups, so simply redirects
            //Included for ease in each step checking for differences
            foreach(var item in to.Item)
            {
                if (item.Id == from.Message.ItemNo) MapFromContext(from, item);
            }
        }

        public void MapFromContext(ConsumeContext<ItemEntityUpdated> from, Item to)
        {
            to.Name = from.Message.Name;
            to.Price = from.Message.Price;
        }
    }
}