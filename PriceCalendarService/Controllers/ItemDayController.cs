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
    public class ItemDayController : ControllerBase
    {
        private readonly IItemDayService _service;
        public ItemDayController(IItemDayService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAll());
        }


        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(ItemDayListDTO dto)
        {
            return Ok(await _service.Add(dto));
        }
    }
}