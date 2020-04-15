using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PriceCalendarService.Models;

namespace PriceCalendarService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PriceCalendarController : ControllerBase
    {
        public PriceCalendarController() 
        {

        }
        [HttpGet]
        public async Task<IActionResult> GetGroups() 
        {
            var item = new ItemPriceAndCurrencyResponse();
            item.Currency = "DA";
            return Ok(item);
        }
    
    }
}