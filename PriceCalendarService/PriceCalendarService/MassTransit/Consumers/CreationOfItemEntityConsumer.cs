﻿using AutoMapper;
using ContractsV2.ItemContracts;
using ItemContracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using PriceCalendarService.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.MassTransit.Consumers
{
    public class CreationOfItemEntityConsumer : IConsumer<IItemEntityCreated>
    {
        private readonly PriceCalendarServiceContext _serviceContext;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CreationOfItemEntityConsumer(PriceCalendarServiceContext context, IMapper mapper, ILogger logger)
        {
            _serviceContext = context;
            _mapper = mapper;
            _logger = logger;
        }

        //public CreationOfItemEntityConsumer() { }

        public Task Consume(ConsumeContext<IItemEntityCreated> consumedContext)
        {
            _logger.Information("PriceService - Received IItemEntityCreated event with itemNo: {ItemNo} ", consumedContext.Message.ItemNo);
            Console.WriteLine("Context received: " + consumedContext.Message + "\nCreating....");

            this.CreateFromContext(consumedContext);

            return Task.CompletedTask;
        }

        public void CreateFromContext(ConsumeContext<IItemEntityCreated> consumedContext)
        {
            Console.WriteLine("Inside db context...");
            //if (!this.CanBeTranslated(consumedContext)) return;
            Console.WriteLine("Context can be translated...");
            Console.WriteLine("Checking for existing relations...");
            var model = _serviceContext.ItemPriceAndCurrencyResponse
                .Include(o => o.Groups)
                .ThenInclude(g => g.Item)
                .FirstOrDefault(c => c.Id == consumedContext.Message.RelationNo);
            Console.WriteLine("Service context found: " + model);
            
            if(model != null && model.Groups != null)
            {
                Console.WriteLine("Comparable response and group found...");
                var item = this.MapFromContext_ExistingFromResponse(consumedContext, model);
                Console.WriteLine("Item being inserted: " + item);
                 _serviceContext.Item.Add(item);
                var stateEntries =  _serviceContext.SaveChanges();
                Console.WriteLine("State entries written: " + stateEntries);
            }
            else if(model != null && model.Groups == null)
            {
                Console.WriteLine("Comparable response found - group null...");
                var group = this.MapFromContext_ExistingFromGroups(consumedContext, model);
                Console.WriteLine("Group being inserted: " + group);
                 _serviceContext.Groups.Add(group);
                var stateEntries =  _serviceContext.SaveChanges();
                Console.WriteLine("State entries written: " + stateEntries);
            }
            else if(model == null)
            {
                Console.WriteLine("No comparable response found...");
                var itemPriceAndCurrencyResponse = this.MapFromContext_NonExisting(consumedContext);
                Console.WriteLine("Response being inserted: " + itemPriceAndCurrencyResponse);
                var stateEntries = _serviceContext.ItemPriceAndCurrencyResponse.Add(itemPriceAndCurrencyResponse);
                _serviceContext.SaveChanges();
                Console.WriteLine("State entries written: " + stateEntries);
            }
        }

        public Item MapFromContext_ExistingFromResponse(ConsumeContext<IItemEntityCreated> from, ItemPriceAndCurrencyResponse existing)
        {
            var item = new Item();
            item.Id = from.Message.ItemNo;
            item.Name = from.Message.Name;
            item.Price = from.Message.Price;
            item.GroupId = from.Message.ArticleGroup;
            foreach(var group in existing.Groups) { if (group.Id == item.GroupId) item.Group = group; }
            return item;
        }

        public Groups MapFromContext_ExistingFromGroups(ConsumeContext<IItemEntityCreated> from, ItemPriceAndCurrencyResponse existing)
        {
            var group = new Groups
            {
                Id = from.Message.ArticleGroup ?? default(int),
                Currency = existing,
                CurrencyId = existing.Id,
                Item = new List<Item>(),
                Description = from.Message.PriceModel
            };

            var item = new Item
            {
                Group = group,
                GroupId = group.Id,
                Id = from.Message.ItemNo,
                Name = from.Message.Name,
                Price = from.Message.Price
            };

            group.Item.Add(item);
            return group;
        }

        public ItemPriceAndCurrencyResponse MapFromContext_NonExisting(ConsumeContext<IItemEntityCreated> from)
        {
            var itemPriceAndCurrencyResponse = new ItemPriceAndCurrencyResponse
            {
                Id = from.Message.RelationNo ?? default(int),
                Currency = from.Message.Unit
            };
            itemPriceAndCurrencyResponse.Groups = new List<Groups>();
            itemPriceAndCurrencyResponse.Groups.Add(this.MapFromContext_ExistingFromGroups(from, itemPriceAndCurrencyResponse));
            return itemPriceAndCurrencyResponse;
        }

        public int GenerateIntId()
        {
            Random rand = new Random();
            return rand.Next(0, 999999);
        }

        public bool CanBeTranslated(ConsumeContext<IItemEntityCreated> context)
        {
            if (context.Message.ArticleGroup == 0 || context.Message.ArticleGroup == null) return false;
            if (string.IsNullOrWhiteSpace(context.Message.ItemNo)) return false;
            if (context.Message.RelationNo == 0 || context.Message.RelationNo == null) return false;
            return true;
        }
    }
}
