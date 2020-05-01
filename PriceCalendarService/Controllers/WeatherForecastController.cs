using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using PriceCalendarService.Hubs;
using PriceCalendarService.Models;

namespace PriceCalendarService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public WeatherForecastController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
          
            await _hubContext.Clients.All.SendAsync("HELLO" , "Works" );
            return Ok("");
        }
    }
}
