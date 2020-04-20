using ItemManagerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemManagerService.Services
{
    public interface IItemService
    {
        public Task<ServiceResponse<List<Items>>> GetItems();

        public Task<ServiceResponse<Items>> GetSingle(int id);

        public Task<ServiceResponse<Items>> AddItem(Items item);

        public Task<ServiceResponse<Items>> UpdateItems(Items item);

        public Task<ServiceResponse<Items>> DeleteItem(int Id);
    }
}
