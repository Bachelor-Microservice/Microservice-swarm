using ItemManagerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ItemManagerService.Services
{
    public class ItemService : IItemService
    {
        /*using (var context = new ItemManagerServiceContext())
        {
            var result = await context.Items.AddAsync(item);
            var saveResult = context.SaveChanges();
        }*/

        private readonly ItemManagerServiceContext _context;

        public ItemService(ItemManagerServiceContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Items>> AddItem(Items item)
        {
            var serviceResponse = new ServiceResponse<Items>();
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            serviceResponse.Data = item;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Items>> DeleteItem(int Id)
        {
            var serviceResponse = new ServiceResponse<Items>();
            var toBeDeleted = await _context.Items.FirstAsync(c => c.Id == Id);
            _context.Items.Remove(toBeDeleted);
            await _context.SaveChangesAsync();

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
            return serviceResponse;
        }
    }
}
