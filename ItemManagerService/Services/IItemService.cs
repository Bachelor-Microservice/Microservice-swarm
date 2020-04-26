using ItemManagerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemManagerService.Services
{
    public interface IItemService
    {
        Task<ServiceResponse<List<Items>>> GetItems();

        Task<ServiceResponse<Items>> GetSingle(int id);

        Task<ServiceResponse<Items>> AddItem(Items item);

        Task<ServiceResponse<Items>> UpdateItems(Items item);

        Task<ServiceResponse<Items>> DeleteItem(int Id);
    }
}
