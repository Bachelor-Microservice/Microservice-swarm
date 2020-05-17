using BookingService.DTO;
using BookingService.Models;
using BookingService.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger _logger;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
           // _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Booking>>> Get()
        {
           // _logger.LogInformation("TESTLOGGIN");
            return await _bookingService.Get();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _bookingService.Get(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

       /* [HttpPost]
        public async Task<IActionResult> Create([FromBody] Booking s)
        {
            var result = await _bookingService.Create(s);
            return CreatedAtRoute("Get", new { id = s.Id.ToString() }, s);
        }*/

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingDTO s)
        {
            var result = await _bookingService.Create(s);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Booking request)
        {
            var existing = await _bookingService.Get(id);
            if (existing == null) return NotFound();
            request.Id = existing.Id;

            var result = await _bookingService.Update(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _bookingService.Get(id);
            if (existing == null) return NotFound();
            await _bookingService.Remove(id);
            return NoContent();
        }
    }
}
