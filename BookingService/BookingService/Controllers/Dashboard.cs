using System.Collections.Generic;
using System.Threading.Tasks;
using BookingService.Models;
using BookingService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public DashboardController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        
        [HttpGet]
        [Route("arrival")]
        public async Task<ActionResult<List<Booking>>> GetArrivals()
        {
            return await _bookingService.GetArrivalsToday();
        }
        [HttpGet]
        [Route("departue")]
        public async Task<ActionResult<List<Booking>>> GetDepartue()
        {
            return await _bookingService.GetArrivalsToday();
        }
    }
}