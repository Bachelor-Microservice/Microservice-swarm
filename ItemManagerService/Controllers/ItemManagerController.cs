using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriceCalendarService.Models;

namespace ItemManagerService.Controllers
{
    [APIController]
    [Route("[controller]")]
    public class ItemManagerController : ControllerBase
    {
        Public ItemManagerController(){}

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetItemEntities()
        {
            var itemEntity = new ItemEntity();
            itemEntity.Id = 999999;
            return OK(itemEntity);
        }
    }
}