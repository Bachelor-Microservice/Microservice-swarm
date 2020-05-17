using ItemManagerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using ItemContracts;
using Shared.MassTransit.Contracts;
using AutoMapper;
using ItemManagerService.MassTransit.Publishers;
using Serilog;

namespace ItemManagerService.Services
{
    public class ItemService : IItemService
    {
        private readonly ItemManagerServiceContext _context;
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly IPublishItemCrud _publisher;
        private readonly ILogger _logger;

        public ItemService(ItemManagerServiceContext context, IBus bus, IMapper mapper, IPublishItemCrud publisher, 
            ILogger logger)
        {
            _context = context;
            _bus = bus;
            _mapper = mapper;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task<ServiceResponse<Items>> AddItem(Items item)
        {
            item.Id = GetId();
            if (item.ArticleGroup == 0 || item.ArticleGroup == null) item.ArticleGroup = GetId();
            if (item.RelationNo == 0 || item.RelationNo == null) item.RelationNo = GetId();
            if (string.IsNullOrWhiteSpace(item.ItemNo)) item.ItemNo = GenerateRandomLowerCaseNormalizedString(12);
            var serviceResponse = new ServiceResponse<Items>();
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();

            _logger.Information("ItemManagerService - Added item with Id: {ItemNo}", item.ItemNo);

            serviceResponse.Data = item;
            _publisher.Created(item);
            return serviceResponse;
        }

        public async Task<ServiceResponse<Items>> DeleteItem(int Id)
        {
            var serviceResponse = new ServiceResponse<Items>();
            var toBeDeleted = await _context.Items.FirstAsync(c => c.Id == Id);
            _context.Items.Remove(toBeDeleted);
            await _context.SaveChangesAsync();

            _logger.Information("ItemManagerService - removed item with Id: {Id}", Id);

            _publisher.Deleted(Id, toBeDeleted.ItemNo);

            serviceResponse.Data = toBeDeleted;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Items>>> GetItems()
        {
            var serviceResponse = new ServiceResponse<List<Items>>();
            serviceResponse.Data = await _context.Items.ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<Items>> GetSingle(int id)
        {
            var serviceResponse = new ServiceResponse<Items>();
            serviceResponse.Data = await _context.Items.FirstOrDefaultAsync(c => c.Id == id);
            return serviceResponse;
        }

        public async Task<ServiceResponse<Items>> UpdateItems(Items item)
        {
            //var old = await _context.Items.FirstOrDefaultAsync(c => c.Id == item.Id);
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
            var serviceResponse = new ServiceResponse<Items>
            {
                Data = item
            };

            _publisher.Updated(item);

            return serviceResponse;
        }

        public int GetId() 
        {
            Random rand = new Random();
            return rand.Next(0,999999);
        }

        public string GenerateRandomLowerCaseNormalizedString(int length)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}
