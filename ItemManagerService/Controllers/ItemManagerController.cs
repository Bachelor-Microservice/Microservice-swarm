using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItemManagerService.Models;
using System;

namespace ItemManagerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemManagerController : ControllerBase
    {
        public ItemManagerController(){}

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetItemEntities()
        {
            var itemEntity = new ItemEntity();
            itemEntity.Id = 999999;
            return Ok(itemEntity);
        }
    }
}