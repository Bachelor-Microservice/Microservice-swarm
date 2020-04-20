using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ItemManagerService.Entities;
using System;
using ItemManagerService.Services;
using ItemManagerService.Models;

namespace ItemManagerService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemManagerController : ControllerBase
    {
        private readonly IItemService _itemservice;
        
        public ItemManagerController(IItemService service)
        {
            _itemservice = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetItemEntities()
        {
            return Ok(await _itemservice.GetItems());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _itemservice.GetSingle(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(Items item)
        {
            return Ok(await _itemservice.AddItem(item));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem(Items item)
        {
            return Ok(await _itemservice.UpdateItems(item));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteItem(int id)
        {
            return Ok(await _itemservice.DeleteItem(id));
        }
    }
}