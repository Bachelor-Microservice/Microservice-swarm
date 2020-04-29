using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using PriceCalendarService.Models;
using PriceCalendarService.Services;
using PriceCalendarService.Dtos;
using AutoMapper;

namespace PriceCalendarService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PriceCalendarController : ControllerBase
    {
        private readonly IItemPriceAndCurrencyResponseService _service;
        public PriceCalendarController(IItemPriceAndCurrencyResponseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _service.Get(id));
        }


        [HttpPost]
        public async Task<IActionResult> Add(ItemPriceAndCurrencyResponseDTO dto) 
        {
            return Ok(await _service.Add(dto));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ItemPriceAndCurrencyResponseDTO cmd)
        {
            return Ok(await _service.Update(cmd));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _service.Delete(id));
        }
    
    }
}
